using UnityEngine;

public class RacketController : MonoBehaviour
{
    float input;
    public float movementSpeed = 5f;

    public float minScale = 0.25f;
    public float maxScale = 4.0f;

    private Rigidbody2D rigidbody2D; // default = null

    private IInput inputComponent;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        inputComponent = GetComponent<IInput>();
    }

    void Update()
    {
        input = inputComponent.GetInput();
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
