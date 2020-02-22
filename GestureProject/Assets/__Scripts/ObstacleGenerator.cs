using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Single object for now, could use a list later.
    private GameObject obstacle;
    private GameObject fallingTree;
    private GameObject npc;
    private GameObject coin;
    private GameObject container;
    private GameObject revivePotion;

    void Start()
    {
        container = GameObject.Find("GeneratedObjects");

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 0; i < 400; i += 27)
        {
            int rand = Random.Range(0, 2);

            // Load either logs or fences
            if (rand == 0)
            {
                obstacle = Resources.Load("fence") as GameObject;
            }
            else
            {
                obstacle = Resources.Load("logs") as GameObject;
            }

            Instantiate(obstacle, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 100; i < 500; i += 50)
        {
            npc = Resources.Load("NPC_Man") as GameObject;
            Instantiate(npc, new Vector3(Random.Range(-3f, 3f), 1f, i), Quaternion.Euler(0, -180, 0)).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 50; i < 450; i += 100)
        {
            fallingTree = Resources.Load("TriggerObstacle") as GameObject;
            Instantiate(fallingTree, new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
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
