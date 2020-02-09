using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject pathPrefab;
    void Start()
    {
        GameObject copy = pathPrefab;
        GameObject grass = Resources.Load("grass") as GameObject;


        for (int i = -60; i < 0; i += 2)
        {
            //Different grass prefabs can be used randomly but Coords need to be fixed
            grass = Resources.Load("grass1") as GameObject;
            copy.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
            Instantiate(copy, new Vector3(Random.Range(-10.5f, -9.5f), Random.Range(-11f, -10.5f), i), Quaternion.identity);
            Instantiate(grass, new Vector3(Random.Range(36f, 41f), Random.Range(-12f, -11f), i+ Random.Range(10, 15)), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
