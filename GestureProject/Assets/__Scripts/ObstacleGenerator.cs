
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static int numberOfChunks = 0;

    // Single object for now, could use a list later.
    private GameObject obstacle;
    private GameObject fallingTree;
    private GameObject rock;
    private GameObject npc;
    private GameObject coin;
    private GameObject container;
    private GameObject revivePotion;

    void Start()
    {
        GenerateVillageObjects();
        GenerateRiverObjects();
        //GenerateForestObjects();

        container = GameObject.Find("GeneratedObjects");

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 30; i < 400; i += 27)
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

        SpawnNPCS();
        SpawnTrees();
        SpawnCoins();
        SpawnHelmets();
        SpawnPotions();

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 500; i < 900; i += 55)
        {
            rock = Resources.Load("rock") as GameObject;
            Instantiate(rock, new Vector3(Random.Range(-4f, 4f), 1.2f, i), Quaternion.identity).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        //for (int i = 100; i < 500; i += 50)
        //{
        //    npc = Resources.Load("NPC_Man") as GameObject;
        //    Instantiate(npc, new Vector3(Random.Range(-3f, 3f), 1f, i), Quaternion.Euler(0, -180, 0)).transform.parent = container.transform;
        //}

        // Basic implementation for now, will need to be improved and randomized.
        //for (int i = 50; i < 500; i += 150)
        //{
        //    fallingTree = Resources.Load("TriggerObstacle") as GameObject;
        //    Instantiate(fallingTree, new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        //}

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 30; i < 300; i += 50)
        {
            coin = Resources.Load("coin") as GameObject;
            Instantiate(coin, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

    }

    #region == Chunk Object Generators == 
    public void GenerateVillageObjects()
    {

        numberOfChunks++;
    }

    public void GenerateForestObjects()
    {

        numberOfChunks++;
    }

    public void GenerateRiverObjects()
    {

        numberOfChunks++;
    }
    #endregion

    #region == Single Object Spawners == 
    private void SpawnHelmets()
    {
    }

    private void SpawnPotions()
    {
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = 30; i < 300; i += 100)
        {
            revivePotion = Resources.Load("RevivePotion") as GameObject;
            Instantiate(revivePotion, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }

    private void SpawnCoins()
    {
    }

    private void SpawnTrees()
    {
    }

    private void SpawnNPCS()
    {
    }
    #endregion
}
