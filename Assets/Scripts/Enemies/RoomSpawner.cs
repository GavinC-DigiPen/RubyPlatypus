//------------------------------------------------------------------------------
//
// File Name:	RoomSpawner.cs
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

public class RoomSpawner : MonoBehaviour
{
    [Tooltip("The top left corner of the area")]
    public Vector2 topLeft;
    [Tooltip("The bottom tight of the area")]
    public Vector2 bottomRight;
    [Tooltip("The enemy prefabs")]
    public GameObject[] enemies;
    [Tooltip("The number of enemies spawned for each prefab")]
    public int[] numEnemies;
    [Tooltip("The chest prefab")]
    public GameObject chestPrefab;
    [Tooltip("Percent chance of spawning a chest (0-100)")]
    public float chestSpawnChance = 25;

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

        if (chestPrefab)
        {
            float randomSpawnChance = Random.Range(0f, 100f);
            if (randomSpawnChance < chestSpawnChance)
            {
                Vector2 spawnLocation;
                spawnLocation.x = Random.Range(topLeft.x, bottomRight.x);
                spawnLocation.y = Random.Range(bottomRight.y, topLeft.y);
                Instantiate(chestPrefab, spawnLocation, Quaternion.identity);
            }
        }

        Destroy(this);   
    }
}
