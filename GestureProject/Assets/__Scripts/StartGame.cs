using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool gameStarted = false;
    private float jumpSpeed = 4.5f;
    private GameObject mainCamera;
    private GameObject player;
    private Rigidbody playerRb;
    public Animator animator;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Low Poly Warrior");
        playerRb = player.GetComponent<Rigidbody>();
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
            // Check if player wants to jump.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerJump();
            }
        }
    }
    
    // Make the player jump.
    private void PlayerJump()
    {
        playerRb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpSpeed, ForceMode.Impulse);
    }
    
    private void OnMouseDown()
    {
        gameStarted = true;
        Debug.Log("Game Started");
    }
}
