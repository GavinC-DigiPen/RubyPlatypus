//------------------------------------------------------------------------------
//
// File Name:	Timer.cs
// Author(s):	Nathan Stern (nathan.stern@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright � 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Timer : MonoBehaviour
{
    public float switchTime = 15f;
    private float timer = 0f;
    public Color healColor;
    public Color attackColor;
    private bool isOnHealMode = true;
    public TilemapRenderer levelRenderer;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= switchTime)
        {
            timer -= switchTime;
            isOnHealMode = !isOnHealMode;
            levelRenderer.material.color = isOnHealMode ? healColor : attackColor;
            FindObjectOfType<Sword>().attacking = !isOnHealMode;
            FindObjectOfType<Health>().SwapHealing(isOnHealMode);
        }
    }
}
