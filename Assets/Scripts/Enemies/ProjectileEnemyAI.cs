﻿//------------------------------------------------------------------------------
//
// File Name:	ProjectileEnemyAI.cs
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

public class ProjectileEnemyAI : MonoBehaviour
{
    [Tooltip("The health of the enemy")]
    public float currentHealth = 1;
    [Tooltip("The move speed of the enemy")]
    public float moveSpeed = 2;
    [Tooltip("How fast the enemy slows down")]
    public float slowDownSpeed = 0.01f;
    [Tooltip("The range at which the enemy will detect the player")]
    public float detectionRange = 15;
    [Tooltip("The farthest range the enemy will shot at")]
    public float maxShootingRange = 10;
    [Tooltip("The closest range the enemy will shot at")]
    public float minShootingRange = 7;
    [Tooltip("The move speed of the summoned projectile")]
    public float projectileSpeed = 4;
    [Tooltip("The time it takes to shoot a projectile")]
    public float shootTime = 0.25f;
    [Tooltip("The time before the projectile can be shot again")]
    public float shootCooldownTime = 0.5f;
    [Tooltip("The prefab of an object that will be the projectile")]
    public GameObject projectilePrefab;
    [Tooltip("The pushback force")]
    public float pushForce = 4f;

    private Rigidbody2D enemyRB;
    private GameObject target;
    private Vector2 direction;

    private bool shooting = false;

    private bool bouncing = false;
    private Vector2 bounceDir;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
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
            if (shooting == false)
            {
                if (direction.magnitude <= minShootingRange)
                {
                    direction = direction.normalized;
                    enemyRB.velocity = -direction * moveSpeed;
                }
                if (direction.magnitude <= maxShootingRange)
                {
                    Invoke("ShootProjectile", shootTime);
                    shooting = true;
                }
                else if (direction.magnitude <= detectionRange)
                {
                    direction = direction.normalized;
                    enemyRB.velocity = direction * moveSpeed;
                }
                else
                {
                    enemyRB.velocity = Vector2.Lerp(enemyRB.velocity, new Vector2(0, 0), slowDownSpeed);
                }
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
            if (currentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Summon the projecile, rotate it, and add force
    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        float projectileRotation;
        direction = (target.transform.position - transform.position).normalized;
        projectileRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -projectileRotation));
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * projectileSpeed;

        shooting = false;
    }
}
