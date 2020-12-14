using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    // access modifiers: public, private, protected (, internal)

    // abstract -> override MUSS
    // virtual  -> override KANN

    // (sealed)

    public abstract void Activate();
}
