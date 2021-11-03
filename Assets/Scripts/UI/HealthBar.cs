//------------------------------------------------------------------------------
//
// File Name:	HealthBar.cs
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

public class HealthBar : MonoBehaviour
{
    [Tooltip("The game object that is the health bar")]
    public GameObject healthBar;

    private Health healthScript;
    private Vector2 startingPosition;
    private Vector2 startingScale;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = healthBar.transform.localPosition;
        startingScale = healthBar.transform.localScale;
        healthScript = FindObjectOfType<Health>();
        Health.OnHealthChanged.AddListener(UpdateHealthBar);
        UpdateHealthBar();
    }
      
    // Function that updates health bar and text
    void UpdateHealthBar()
    {
        float scaleScaler = (float)healthScript.currentHealth / healthScript.maxHealth;
        float newXScale = startingScale.x * scaleScaler;

        healthBar.transform.localScale = new Vector2(newXScale, startingScale.y);
        healthBar.transform.localPosition = new Vector2(startingPosition.x - (startingScale.x - newXScale) / 2f, startingPosition.y);
    }
}
