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

    private Transform playerTransform;
    public float ballRacketOffset = 0.7f;

    // static = not an instance member field
    private static int ActiveBallsInScene = 0;


    private void Awake()
    {
        initialPosition = transform.position;

        rigidbody2D = GetComponent<Rigidbody2D>();

        ActiveBallsInScene++;
    }

    private void Start()
    {
        // check if other balls exist?
        if (ActiveBallsInScene > 1)
        {
            started = true;
        } else
        {
            // if not:
            started = false;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            } else
            {
                Debug.LogWarning("Could not find any Player GameObject.");
            }
        }
    }

    private void Update()
    {
        if (!started)
        {
            transform.position = playerTransform.position + Vector3.up * ballRacketOffset;

            if (Input.GetButtonDown("Jump"))
            {
                StartBall();
            }
        }
    }

    private void OnDestroy()
    {
        ActiveBallsInScene--;
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
        if (ActiveBallsInScene > 1)
        {
            Destroy(gameObject);
        } else
        {
            StopAndResetBall();
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, Vector3.right * rigidbody2D.velocity.x);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, Vector3.up * rigidbody2D.velocity.y);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(Vector3.zero, rigidbody2D.velocity);
    }

    public BallController CreateDuplicate()
    {
        if (!started) return null;

        BallController newBall = Instantiate(this);

        // flip x velocity of new ball
        Vector2 newVelocity = new Vector2(
            -rigidbody2D.velocity.x,
            rigidbody2D.velocity.y
            );

        // random x velocity when x velocity near 0
        if (Mathf.Approximately(newVelocity.x, 0))
        {
            newVelocity.x = Random.Range(0.1f, 1f) * Mathf.Sign(Random.Range(-1, 1));
            
            // TODO: also apply new x velocity to this ball
        }

        newBall.SetVelocity(newVelocity.normalized * initialSpeed);
        newBall.SetPlayerTransform(playerTransform);

        return newBall;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;
    }

}
