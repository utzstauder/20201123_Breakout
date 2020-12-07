using UnityEngine;

public class Brick : MonoBehaviour
{
    private BrickSpawner spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnBrickHit();
    }

    private void OnDestroy()
    {
        spawner.OnBeforeBrickDestroyed(this);
    }

    public void SetBrickSpawner(BrickSpawner spawner)
    {
        this.spawner = spawner;
    }

    protected virtual void OnBrickHit()
    {
        Destroy(gameObject);
    }
}
