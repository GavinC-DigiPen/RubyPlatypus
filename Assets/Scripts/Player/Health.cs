//------------------------------------------------------------------------------
//
// File Name:	Health.cs
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
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Tooltip("The current health of the player")]
    public int currentHealth = 10;
    [Tooltip("The scene that is loaded when the player dies")]
    public string nextSceneName;
    [Tooltip("The amount of the time play will be invincible after getting hit")]
    public float invincibilityFramesTime = 0.5f;

    private int maxHealth;
    private float invincibilityFramesTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityFramesTimer -= Time.deltaTime;
    }

    // Increase the current health
    // Parms:
    //  amount: the amount the current health will increase
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
    }

    // Decrease the current health
    // Parms:
    //  amount: the amount the current health will decrease
    public void DecreaseHealth(int amount)
    {
        if (invincibilityFramesTimer <= 0)
        {
            currentHealth -= amount;
            invincibilityFramesTimer = invincibilityFramesTime;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
