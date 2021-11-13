//------------------------------------------------------------------------------
//
// File Name:	EnemyDamage.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Tooltip("The amount of damage the gameObject will do")]
    public int damage = 1;

    private float damageTimer;
    private float damageInterval = 1f;
    private bool damageActive = false;

    private void Update()
    {
        if(damageActive)
        {
            damageTimer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        damageActive = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        damageActive = false;
        damageTimer = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(damageTimer > damageInterval)
            {
                damageTimer = 0;
                collision.gameObject.GetComponent<Health>().ChangeHealth((int)(-damage * ((GameManager.Loop / 4f) + 1)));
            }
        }
    }
}
