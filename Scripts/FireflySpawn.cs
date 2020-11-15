using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawn : MonoBehaviour
{
    void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-30f, 0f), Random.Range(1f, 8f), Random.Range(-37f, 6f));

        transform.rotation = Random.rotation;

    }
}
