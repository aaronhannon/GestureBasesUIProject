using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private static int numberOfChunks = 1;
    private const int chunkLenght = 500;

    // Single object for now, could use a list later.
    private GameObject obstacle;
    private GameObject fallingTree;
    private GameObject rock;
    private GameObject npc;
    private GameObject coin;
    private GameObject revivePotion;

    private GameObject container;

    void Start()
    {
        container = GameObject.Find("GeneratedObjects");

        GenerateVillageObjects();
        GenerateRiverObjects();
        //GenerateForestObjects();
    }

    
    #region == Chunk Object Generators == 
    public void GenerateVillageObjects()
    {
        int start = (numberOfChunks - 1) * 400;
        int end = numberOfChunks * 400;

        for (int i = start; i < end; i += 50)
        {
            npc = Resources.Load("NPC_Man") as GameObject;
            Instantiate(npc, new Vector3(Random.Range(-3f, 3f), 1f, i), Quaternion.Euler(0, -180, 0)).transform.parent = container.transform;
        }

        // Basic implementation for now, will need to be improved and randomized.
        for (int i = start; i < end; i += 27)
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

        // Testing to show trees for now. Will be moved to forest.
        //for (int i = start; i < end; i += 150)
        //{
        //    fallingTree = Resources.Load("TriggerObstacle") as GameObject;
        //    Instantiate(fallingTree, new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        //}
        
        SpawnCoins(start, end);
        SpawnPotions(start, end);

        numberOfChunks++;
    }

    public void GenerateForestObjects()
    {
        int start = (numberOfChunks - 1) * 400;
        int end = numberOfChunks * 400;

        for (int i = start; i < end; i += 150)
        {
            fallingTree = Resources.Load("TriggerObstacle") as GameObject;
            Instantiate(fallingTree, new Vector3(0.5f, 2.5f, i), Quaternion.identity).transform.parent = container.transform;
        }

        SpawnCoins(start, end);
        SpawnPotions(start, end);

        numberOfChunks++;
    }

    public void GenerateRiverObjects()
    {
        Debug.Log(numberOfChunks);

        int start = (numberOfChunks - 1) * 400;
        Debug.Log(start);

        int end = numberOfChunks * 400;
        Debug.Log(end);

        for (int i = start; i < end; i += 55)
        {
            rock = Resources.Load("rock") as GameObject;
            Instantiate(rock, new Vector3(Random.Range(-4f, 4f), 1.2f, i), Quaternion.identity).transform.parent = container.transform;
        }

        SpawnCoins(start, end);
        SpawnPotions(start, end);

        numberOfChunks++;
    }
    #endregion

    #region == Single Object Spawners == 
   
    private void SpawnHelmets()
    {

    }

    private void SpawnPotions(int start, int end)
    {
        // Basic implementation for now, will need to be improved and randomized.
        for (int i = start; i < end; i += 100)
        {
            revivePotion = Resources.Load("RevivePotion") as GameObject;
            Instantiate(revivePotion, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }

    private void SpawnCoins(int start, int end)
    {
        for (int i = start ; i < end; i += 50)
        {
            coin = Resources.Load("coin") as GameObject;
            Instantiate(coin, new Vector3(Random.Range(-3f, 3f), 1.5f, i), Quaternion.identity).transform.parent = container.transform;
        }
    }
    #endregion
}
