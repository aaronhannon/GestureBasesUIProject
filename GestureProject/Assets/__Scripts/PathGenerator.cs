using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that deals with generating paths in chunks
public class PathGenerator : MonoBehaviour
{
    public GameObject pathPrefab;
    private GameObject container;
    void Start()
    {
        container = GameObject.Find("GeneratedObjects");
        GameObject copy = pathPrefab;
        GameObject grass;
        
        //spawn paths
        for (int i = 0; i < 420; i += 1)
        {
            copy.transform.localScale = new Vector3(Random.Range(1.5f, 3.0f), 1, Random.Range(1.0f, 2.0f));
            Instantiate(copy, new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), i), Quaternion.identity).transform.parent = container.transform;
        }

        //spawn grass
        for (int i = 0; i < 200; i += 1)
        {
            //Different grass prefabs can be used randomly but Coords need to be fixed
            grass = Resources.Load("grass4") as GameObject;
            Instantiate(grass, new Vector3(Random.Range(-10f, -5f), Random.Range(0f, 1f), i * 2 + Random.Range(10, 15)), Quaternion.identity).transform.parent = container.transform;
        }
    }
}
