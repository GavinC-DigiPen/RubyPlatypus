//------------------------------------------------------------------------------
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
    [Tooltip("The starting health of the enemy")]
    public int startingHealth = 1;
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
    [Tooltip("The time it takes to prep a shot a projectile")]
    public float shootPrepTime = 0.25f;
    [Tooltip("The time it takes to shoot a projectile")]
    public float shootTime = 0.25f;
    [Tooltip("The prefab of an object that will be the projectile")]
    public GameObject projectilePrefab;
    [Tooltip("The shoot sound")]
    public AudioClip shootSound;
    [Tooltip("The distance away from the center of the object the projectile will be summoned")]
    public float prefabDistanceFromCenter = 0.6f;
    [Tooltip("The pushback force")]
    public float pushForce = 4f;
    [Tooltip("Money gained from killing enemy")]
    public int money = 1;

    //[HideInInspector]
    public int maxHealth;
    //[HideInInspector]
    public int currentHealth;

    private Rigidbody2D enemyRB;
    private AudioSource enemyAudio;
    private GameObject target;
    private Vector2 direction;

    private bool shooting = false;
    private bool moving = true;
    private bool bouncing = false;
    private Vector2 bounceDir;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAudio = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");

        maxHealth = startingHealth;
        maxHealth = (int)Mathf.Pow(maxHealth, 1 + GameManager.Loop / 10f);
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
            if (moving == true)
            {
                if (direction.magnitude < minShootingRange)
                {
                    direction = direction.normalized;
                    enemyRB.velocity = -direction * moveSpeed;
                }
                else if (direction.magnitude <= maxShootingRange && shooting == false)
                {
                    Invoke("StartShooting", shootPrepTime);
                    shooting = true;
                }
                else if (direction.magnitude < detectionRange)
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

        float color = currentHealth / (float)maxHealth;
        GetComponent<Renderer>().material.color = new Color(color,color,color,1);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
                GameManager.AddMoney(money * 2 * (GameManager.Loop + 1));
                Destroy(gameObject);
            }
        }
    }

    private void StartShooting()
    {
        Invoke("ShootProjectile", shootTime);
        moving = false;
    }

    // Summon the projecile, rotate it, and add force
    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        AOEDamageProjectile damageAOEScript = projectile.GetComponent<AOEDamageProjectile>();
        if (damageAOEScript)
        {
            damageAOEScript.targetLocation = target.transform.position;
        }

        AOEHealingProjectile healingAOEScript = projectile.GetComponent<AOEHealingProjectile>();
        if(healingAOEScript)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int index = -1;
            float closestDistance = detectionRange;
            for (int i = 0; i < enemies.Length; i++)
            {
                if ((enemies[i].transform.position - transform.position).magnitude < closestDistance)
                {
                    if (enemies[i].transform.position != transform.position)
                    {
                        index = i;
                        closestDistance = (enemies[i].transform.position - transform.position).magnitude;
                    }
                }
            }

            Vector2 enemyLocation;
            if(index == -1)
            {
                enemyLocation = transform.position;
            }
            else
            {
                enemyLocation = enemies[index].transform.position;
            }
            
            healingAOEScript.targetLocation = enemyLocation;
            direction = (enemyLocation - (Vector2)transform.position).normalized;
        }
        else
        {
            direction = (target.transform.position - transform.position).normalized;
        }

        float projectileRotation;
        projectileRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -projectileRotation));
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * projectileSpeed;

        projectile.transform.position = projectile.transform.position + (Vector3)direction * prefabDistanceFromCenter;

        enemyAudio.clip = shootSound;
        enemyAudio.Play();

        shooting = false;
        moving = true;
    }
}
