using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    KeyCode attack = KeyCode.Space;
    float attackTime = 0f;
    public float cooldown = 0.25f;
    public float swordOffTime = 0.1f;
    public GameObject sword;

    void Update()
    {
        attackTime += Time.deltaTime;
        if (attackTime > swordOffTime)
        {
            sword.GetComponent<SpriteRenderer>().enabled = false;
            sword.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        if (Input.GetKey(attack))
        {
            if (sword.GetComponent<Sword>().attacking && attackTime > cooldown)
            {
                attackTime = 0;
                Attack();
            }
        }
    }

    void Attack()
    {
        sword.GetComponent<SpriteRenderer>().enabled = true;
        sword.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
