using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject pathPrefab;
    void Start()
    {
        GameObject copy = pathPrefab;
        GameObject grass;
        
        
        for (int i = -60; i < 200; i += 2)
        {
            //Different grass prefabs can be used randomly but Coords need to be fixed
            grass = Resources.Load("grass4") as GameObject;
            copy.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
            Instantiate(copy, new Vector3(Random.Range(-10.5f, -9.5f), Random.Range(-11f, -10.5f), i), Quaternion.identity);
            Instantiate(grass, new Vector3(Random.Range(-18f, -15f), Random.Range(-10f, -11f), i+ Random.Range(10, 15)), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
