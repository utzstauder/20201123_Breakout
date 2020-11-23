using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private BrickSpawner spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        spawner.OnBeforeBrickDestroyed(this);
    }

    public void SetBrickSpawner(BrickSpawner spawner)
    {
        this.spawner = spawner;
    }
}
