//------------------------------------------------------------------------------
//
// File Name:	Movement.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    RubyPlatypus
// Course:	    WANIC VGP2
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("The maximum speed")]
    public float maxSpeed = 5f;
    [Tooltip("The acceleration")]
    public float acceleration = 0.5f;
    [Tooltip("How fast the player slows down when not moving (0 - 1)")]
    public float slowDown = 0.01f;

    private Rigidbody2D playerRB;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;

    private Vector2 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;
        direction.x = MostRecentKey(leftKey, rightKey);
        direction.y = MostRecentKey(downKey, upKey);

        Vector2 newVelocity = playerRB.velocity;
        newVelocity.x += acceleration * direction.x;
        newVelocity.y += acceleration * direction.y;

        if (direction.x == 0)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, slowDown);
        }
        if (direction.y == 0)
        {
            newVelocity.y = Mathf.Lerp(newVelocity.y, 0, slowDown);
        }

        playerRB.velocity = newVelocity;
        playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxSpeed);

        Vector2 currentPosition = transform.position;
        Vector2 deltaPosition = currentPosition - lastPosition;
        if (deltaPosition != Vector2.zero)
        {
            Vector2 lookDirection = (deltaPosition).normalized;
            float angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        }
        lastPosition = currentPosition;
    }

    // Find the most recent key pushed or currently pushed
    // Parms:
    //  negative: the key that will result in a -1 returned
    //  possitive: the key that will result in a 1 returned
    // Returns:
    //  -1 or 1 based on which button is currently active, 0 if no key
    private int MostRecentKey(KeyCode negative, KeyCode positive)
    {
        int direction = 0;
        if (Input.GetKeyDown(negative))
        {
            direction = -1;
        }
        else if (Input.GetKey(negative) && !Input.GetKey(positive))
        {
            direction = -1;
        }
        else if (Input.GetKeyDown(positive))
        {
            direction = 1;
        }
        else if (Input.GetKey(positive) && !Input.GetKey(negative))
        {
            direction = 1;
        }
        else if (!Input.GetKey(negative) && !Input.GetKey(positive))
        {
            direction = 0;
        }

        return direction;
    }
}
