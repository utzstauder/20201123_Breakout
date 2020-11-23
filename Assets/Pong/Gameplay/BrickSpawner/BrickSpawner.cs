using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public Brick brickPrefab;

    public int columns = 1; // x-coordinate
    public int rows = 1;    // y-coordinate

    public float xOffset = 1f;
    public float yOffset = 1f;

    private void Start()
    {
        SpawnBricks();
    }

    void SpawnBricks()
    {
        Vector3 startPosition = transform.position - ((Vector3.right * xOffset * (columns - 1)) + (Vector3.down * yOffset * (rows - 1))) / 2f;

        for (int y = 0; y < rows; y++) // rows
        {
            for (int x = 0; x < columns; x++) // columns
            {
                //Debug.Log(x + "|" + y);

                Instantiate(
                    original: brickPrefab,
                    position: startPosition + (Vector3.right * xOffset * x) + (Vector3.down * yOffset * y),
                    rotation: Quaternion.identity
                    );
            }
        }


    }

    private void OnValidate()
    {
        columns = Mathf.Clamp(columns, 1, columns);
        rows = Mathf.Clamp(rows, 1, rows);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;

        Vector3 startPosition = -((Vector3.right * xOffset * (columns - 1)) + (Vector3.down * yOffset * (rows - 1))) / 2f;

        for (int y = 0; y < rows; y++) // rows
        {
            for (int x = 0; x < columns; x++) // columns
            {
                Gizmos.DrawWireCube(
                    center: startPosition + (Vector3.right * xOffset * x) + (Vector3.down * yOffset * y),
                    size: new Vector3(1f, 0.5f, 0)
                    );
            }
        }
    }
}
