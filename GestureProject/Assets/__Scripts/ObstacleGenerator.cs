using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Single object for now, could use a list later.
    public GameObject fence;

    void Start()
    {
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = -40; i < 200; i += 25)
        {
            fence = Resources.Load("fence") as GameObject;
            Instantiate(fence, new Vector3(Random.Range(30f, 35f), -8f, i), Quaternion.identity);
        }
    }
}
