//------------------------------------------------------------------------------
//
// File Name:	MeleeEnemyAI.cs
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

[RequireComponent(typeof(Rigidbody2D))]
public class MeleeEnemyAI : MonoBehaviour
{
    [Tooltip("The starting health of the enemy")]
    public int startingHealth = 15;
    [Tooltip("The movespeed of the enemy")]
    public float moveSpeed = 2;
    [Tooltip("How fast the enemy slows down")]
    public float slowDownSpeed = 0.01f;
    [Tooltip("The range at which the enemy will detect the player")]
    public float detectionRange = 10;
    [Tooltip("The pushback force")]
    public float pushForce = 4f;

    //[HideInInspector]
    public int maxHealth;
    //[HideInInspector]
    public int currentHealth;

    private Rigidbody2D enemyRB;
    private GameObject target;
    private Vector2 direction;

    private bool bouncing = false;
    private Vector2 bounceDir;

    // Start is called before the first frame update
    void Start()
    {  
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        maxHealth = startingHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (bouncing)
        {
            bounceDir = Vector2.Lerp(bounceDir, new Vector2(0, 0), slowDownSpeed);
            enemyRB.velocity = bounceDir;
            if (bounceDir.magnitude <= 0.4f)
            {
                bouncing = false;
            }
        }
        else
        {
            direction = target.transform.position - transform.position;
            if (direction.magnitude <= detectionRange)
            {
                direction = direction.normalized;
                enemyRB.velocity = direction * moveSpeed;
            }
            else
            {
                enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
            }
        }
    }

    // Check for collision with sword
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sword swordScript = collision.GetComponent<Sword>();
        if (swordScript)
        {
            currentHealth -= swordScript.damage;
            bouncing = true;
            bounceDir = (enemyRB.position - (Vector2)collision.transform.parent.position).normalized * pushForce;
            if(currentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
