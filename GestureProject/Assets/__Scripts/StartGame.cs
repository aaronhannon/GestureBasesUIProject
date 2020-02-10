using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool gameStarted = false;
    private float jumpSpeed = 4.5f;
    private float distToGround;
    private GameObject mainCamera;
    private GameObject player;
    private Rigidbody playerRb;
    public Animator animator;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Low Poly Warrior");
        playerRb = player.GetComponent<Rigidbody>();
        distToGround = player.GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
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

            // Check if player wants to jump, and if player is on the ground.
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                PlayerJump();
            }

            // Check if player wants to move left (true = left)
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }

            // Check if player wants to move right (false = right)
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }

            // Trying to smooth out movement, will look into this further.
            player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position, 0.5f * Time.deltaTime);
        }
    }
    
    // Check if player is touching the ground by sending a raycast down. 
    // If the distance is greater than .05 then it returns false E.g Player is jumping.
    private bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, distToGround + 0.5f);
    }

    // Make the player jump, and trigger animation.
    private void PlayerJump()
    {
        playerRb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpSpeed, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    private void MoveLeft()
    {
        if(player.transform.position.x >= -11.1f)
        {
            player.transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);

            // Move camera to new player position.
            MoveCamera();
        }
    }

    private void MoveRight()
    {
        if (player.transform.position.x <= -8.1f)
        {
            player.transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y, player.transform.position.z);

            // Move camera to new player position.
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    private void OnMouseDown()
    {
        gameStarted = true;
        Debug.Log("Game Started");
    }
}
