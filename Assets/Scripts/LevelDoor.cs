//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
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

public class LevelDoor : MonoBehaviour
{
    [Tooltip("The button to use the door")]
    public KeyCode transitionKey = KeyCode.E;
    [Tooltip("The rotation rate of the portal")]
    public float rotationRate = 100;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationRate * Time.deltaTime);
    }

    // Check for play collision
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(transitionKey))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                GameManager.Loop++;
            }
        }
    }
}
