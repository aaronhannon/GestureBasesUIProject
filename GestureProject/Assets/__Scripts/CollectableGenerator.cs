using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 0; i < 300; i += 50)
        {
            coin = Resources.Load("coin") as GameObject;
            Instantiate(coin, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
