using UnityEngine;

public class Brick : MonoBehaviour
{
    private BrickSpawner spawner;

    public Powerup[] powerups;

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
        SpawnRandomPowerup();
        Destroy(gameObject);
    }

    protected virtual void SpawnRandomPowerup()
    {
        int randomIndex = Random.Range(0, powerups.Length);
        Instantiate(powerups[randomIndex], transform.position, Quaternion.identity);
    }
}
