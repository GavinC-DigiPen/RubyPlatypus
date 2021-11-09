//------------------------------------------------------------------------------
//
// File Name:	BossSpawner.cs
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

public class BossSpawner : MonoBehaviour
{
    [Tooltip("The boss prefab")]
    public GameObject bossPrefab;
    [Tooltip("The min-boss prefab")]
    public GameObject minibossPrefab;
    [Tooltip("The locations the boss can spawn in")]
    public Vector2[] spawnLocations;
        
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, spawnLocations.Length);
        Instantiate(bossPrefab, spawnLocations[index], Quaternion.identity);

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (i != index)
            {
                Instantiate(minibossPrefab, spawnLocations[i], Quaternion.identity);
            }
        }

        Destroy(this);
    }
}
