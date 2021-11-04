//------------------------------------------------------------------------------
//
// File Name:	EnemySpawner.cs
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

public class EnemySpawner : MonoBehaviour
{
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public GameObject[] enemies;
    public int[] numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        if (enemies.Length == numEnemies.Length)
        {
            Vector2 spawnLocation;
            for (int i = 0; i < enemies.Length; i++)
            {
                for (int j = 0; j < numEnemies[i]; j++)
                {
                    spawnLocation.x = Random.Range(topLeft.x, bottomRight.x);
                    spawnLocation.y = Random.Range(bottomRight.y, topLeft.y);
                    Instantiate(enemies[i], spawnLocation, Quaternion.identity);
                }
            }
        }
        else
        {
            Debug.LogError("Length of enemies and numEnemies is different");
        }
        Destroy(gameObject);   
    }
}
