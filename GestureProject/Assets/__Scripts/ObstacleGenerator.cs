using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Single object for now, could use a list later.
    private GameObject fence;
    private GameObject fallingTree;
    private GameObject npc;
    private GameObject coin;
    private GameObject container;
    private GameObject revivePotion;

    void Start()
    {
        container = GameObject.Find("GeneratedObjects");

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 0; i < 240; i += 25)
        {
            fence = Resources.Load("fence") as GameObject;
            Instantiate(fence, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 100; i < 800; i += 50)
        {
            npc = Resources.Load("NPC_Man") as GameObject;
            Instantiate(npc, new Vector3(Random.Range(-3f, 3f), 1f, i), Quaternion.Euler(0, -180, 0)).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 50; i < 450; i += 100)
        {
            fallingTree = Resources.Load("TriggerObstacle") as GameObject;
            Instantiate(fallingTree, new Vector3(0.2f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 0; i < 300; i += 50)
        {
            coin = Resources.Load("coin") as GameObject;
            Instantiate(coin, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 0; i < 300; i += 100)
        {
            revivePotion = Resources.Load("RevivePotion") as GameObject;
            Instantiate(revivePotion, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }
}
