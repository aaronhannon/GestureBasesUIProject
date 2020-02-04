using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject pathPrefab;
    void Start()
    {
        GameObject copy = pathPrefab;

        

        for (int i = -60; i < -40; i += 2)
        {

            copy.transform.localScale = new Vector3(Random.Range(1.0f, 2.0f), 1, Random.Range(1.0f, 2.0f));
            Instantiate(copy, new Vector3(Random.Range(-10.5f, -9.5f), Random.Range(-10.5f, -9.5f), i), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
