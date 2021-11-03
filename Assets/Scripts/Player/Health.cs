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
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("The scene that is loaded when the player dies")]
    public string nextSceneName;
    [Tooltip("The amount of the time play will be invincible after getting hit")]
    public float invincibilityFramesTime = 0.5f;
    [Tooltip("The amount of the time before regenerating health")]
    public float regenTime = 1f;
    [Tooltip("The amount of health you heal")]
    public int regenAmount = 1;

    [HideInInspector]
    public bool healing = false;

    public int startingHealth = 10;

    public int maxHealth;
    public int currentHealth;

    private float invincibilityFramesTimer = 0;
    private float regenTimer = 0f;

    public static UnityEvent OnHealthChanged = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = startingHealth * (GameManager.Loop + 1); // + Items.MaxHealth;
        currentHealth = maxHealth;
    }

    public void SwapHealing(bool isHealing)
    {
        healing = isHealing;
        regenTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(healing)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenTime)
            {
                regenTimer -= regenTime;
                ChangeHealth(regenAmount);
            }
        }
        invincibilityFramesTimer += Time.deltaTime;
    }

    // Changes the current health by value
    // Parms:
    //  amount: the amount the current health will change
    public void ChangeHealth(int amount)
    {
        if (amount < 0 && invincibilityFramesTimer >= invincibilityFramesTime)
        {
            invincibilityFramesTimer -= invincibilityFramesTime;
        }
        else if(amount < 0)
        {
            return;
        }
        currentHealth += amount;
        OnHealthChanged.Invoke();

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
