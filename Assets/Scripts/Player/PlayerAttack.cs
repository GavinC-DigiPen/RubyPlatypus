//------------------------------------------------------------------------------
//
// File Name:	PlayerAttack.cs
// Author(s):	Nathan Stern (nathan.stern@digipen.edu)
//              Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Cooldown between attacks")]
    public float cooldown = 0.25f;
    [Tooltip("The time the sword is on when swinging")]
    public float swordOffTime = 0.1f;
    [Tooltip("The sword game object")]
    public GameObject sword;
    [Tooltip("The sound palyed when attacking")]
    public AudioClip attackSound;

    private KeyCode attack = KeyCode.Space;

    private AudioSource swordAudio;

    private float attackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        swordAudio = sword.GetComponent<AudioSource>();
    }

    // Update is called once per frame

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

        //swordAudio.clip = attackSound;
        //swordAudio.Play();
    }   
}
