using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable()");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy()");
    }
}
