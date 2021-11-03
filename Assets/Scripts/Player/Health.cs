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

    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }
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
        currentHealth -= amount;
    }
}
