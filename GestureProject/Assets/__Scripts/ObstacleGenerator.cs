using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Single object for now, could use a list later.
    private GameObject fence;
    private GameObject npc;

    void Start()
    {
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 0; i < 240; i += 25)
        {
            fence = Resources.Load("fence") as GameObject;
            Instantiate(fence, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity);

            npc = Resources.Load("NPC_Man") as GameObject;
            Instantiate(npc, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.Euler(0, -180, 0));
        }
    }
}
