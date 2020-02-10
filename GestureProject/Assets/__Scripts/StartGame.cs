using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    private bool gameStarted = false;
    private GameObject mainCamera;
    private GameObject player;
    public Animator animator;
    void Start()
    {
        
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Low Poly Warrior");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted == true)
        {
            if(mainCamera.transform.position.x < -10)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x+0.01f, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                animator.SetBool("Started", true);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.2f);
            }

        }
    }

    private void OnMouseDown()
    {
        gameStarted = true;
        Debug.Log("Game Started");
    }
}
