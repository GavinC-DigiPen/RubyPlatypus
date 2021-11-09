//------------------------------------------------------------------------------
//
// File Name:	AOEHealingProjectile.cs
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

public class AOEHealingProjectile : MonoBehaviour
{
    [Tooltip("The distance the projectile must be at to the location before activating")]
    public float distancePrecision = 0.5f;
    [Tooltip("The scale when doing damage")]
    public Vector2 scale = new Vector2(3, 3);
    [Tooltip("How often the AOE does damage")]
    public float healInterval = 2f;
    [Tooltip("The amount of damage the gameObject will do")]
    public int heal = 1;

    public Vector2 targetLocation;

    private Rigidbody2D projectileRB;
    private CircleCollider2D colliderAOE;

    private Vector2 direction;
    private float healTimer;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        colliderAOE = GetComponent<CircleCollider2D>();
        colliderAOE.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            direction = (Vector2)transform.position - targetLocation;
            if (direction.magnitude <= distancePrecision)
            {
                projectileRB.velocity = Vector2.zero;
                transform.localScale = scale;
                moving = false;
            }
        }
        else
        {
            if (healTimer >= healInterval)
            {
                colliderAOE.enabled = true;
            }
        }
        healTimer += Time.deltaTime;
    }

    // Check for collision with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MeleeEnemyAI meleeScript = collision.gameObject.GetComponent<MeleeEnemyAI>();
        if (meleeScript)
        {
            meleeScript.currentHealth++;
            colliderAOE.enabled = false;
            healTimer = 0;
        }

        ProjectileEnemyAI projectileScript = collision.gameObject.GetComponent<ProjectileEnemyAI>();
        if (projectileScript)
        {
            projectileScript.currentHealth++;
            colliderAOE.enabled = false;
            healTimer = 0;
        }

    }
}
