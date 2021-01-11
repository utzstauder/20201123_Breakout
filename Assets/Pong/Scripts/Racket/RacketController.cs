﻿using UnityEngine;

public class RacketController : MonoBehaviour
{
    float input;
    public float movementSpeed = 5f;

    public float minScale = 0.25f;
    public float maxScale = 4.0f;

    public string axisName;
    public bool aiControlled;
    public Transform aiMovementTarget; // default = null

    private Rigidbody2D rigidbody2D; // default = null

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (aiControlled)
        {
            if (aiMovementTarget != null)
            {
                if (aiMovementTarget.position.x < transform.position.x)
                {
                    // move right
                    input = 1f;
                } else
                {
                    // move left
                    input = -1f;
                }
            } else
            {
                //GameObject targetObject = GameObject.Find("Ball");

                BallController ball = GameObject.FindObjectOfType<BallController>();

                if (ball != null)
                {
                    aiMovementTarget = ball.transform;
                } else
                {
                    Debug.LogError("AiMovementTarget has not been assigned!");
                }
            }

        } else
        {
            input = Input.GetAxisRaw(axisName);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = Vector2.right * input * movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Powerup powerup = collision.gameObject.GetComponent<Powerup>();
        if (powerup != null)
        {
            powerup.Activate();
        }
    }

    public void ScaleHorizontally(float scaleFactor)
    {
        Vector3 newScale = transform.localScale;

        //newScale.x *= scaleFactor;
        //if (newScale.x < minScale) { newScale.x = minScale; }
        //else if (newScale.x > maxScale) { newScale.x = maxScale; }

        newScale.x = Mathf.Clamp(newScale.x * scaleFactor, minScale, maxScale);

        transform.localScale = newScale;
    }
}
