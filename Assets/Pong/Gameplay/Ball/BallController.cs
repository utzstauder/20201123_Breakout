using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private Vector3 initialPosition;
    private bool started = false;

    public float initialSpeed = 5f;
    [Range(0, 90f)]
    public float initialMaxAngle = 45f;

    public float racketDeviationScale = 3f;
    public float wallDeviationScale = 3f;

    public float collisionSpeedMultiplier = 1.1f;

    private void Awake()
    {
        initialPosition = transform.position;

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !started)
        {
            StartBall();
        }
    }

    public void StartBall()
    {
        rigidbody2D.velocity = GetInitialDirection() * initialSpeed;
        started = true;
    }

    public void StopAndResetBall()
    {
        rigidbody2D.velocity = Vector2.zero;
        transform.position = initialPosition;
        started = false;
    }

    private Vector2 GetInitialDirection()
    {
        return Vector2.up;

        // Manuel's solution
        Vector2 newVector = new Vector2(
            x: Random.Range(-Mathf.Tan(initialMaxAngle * Mathf.PI / 180), Mathf.Tan(initialMaxAngle * Mathf.PI / 180)),
            y: 1f
            );

        return newVector.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // calculate deviation factor
            float deviationFactor = transform.position.x - collision.gameObject.transform.position.x;

            // normalize deviationFactor
            deviationFactor /= (collision.collider.bounds.size.x + collision.otherCollider.bounds.size.x) / 2f;

            // copy existing velocity vector
            Vector2 newVelocity = rigidbody2D.velocity;

            // modify y-component of velocity vector
            newVelocity.x += deviationFactor * racketDeviationScale;

            // normalize resulting vector
            newVelocity.Normalize();

            // "restore" initial velocity
            newVelocity *= (rigidbody2D.velocity.magnitude * collisionSpeedMultiplier);

            // set new velocity
            rigidbody2D.velocity = newVelocity;
        }
        else
        {
            if (Mathf.Abs(rigidbody2D.velocity.normalized.y) < 0.1f)
            {
                // calculate deviation factor
                float deviationFactor = collision.gameObject.transform.position.y - transform.position.y;

                // normalize deviationFactor
                deviationFactor /= (collision.collider.bounds.size.y + collision.otherCollider.bounds.size.y) / 2f;

                // help a little in the center of the field
                if (Mathf.Abs(deviationFactor) < 0.1f)
                {
                    deviationFactor = 0.5f * Mathf.Sign(deviationFactor);
                }

                // copy existing velocity vector
                Vector2 newVelocity = rigidbody2D.velocity.normalized;

                // modify x-component of velocity vector
                newVelocity.y = deviationFactor * wallDeviationScale;

                // normalize resulting vector
                newVelocity.Normalize();

                // "restore" initial velocity
                newVelocity *= rigidbody2D.velocity.magnitude;

                // set new velocity
                rigidbody2D.velocity = newVelocity;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAndResetBall();
    }
}
