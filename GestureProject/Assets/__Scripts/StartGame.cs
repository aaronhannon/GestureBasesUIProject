using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private static bool controlsOn = false;
    public static bool ControlsOn
    {
        get { return controlsOn; }
        set { controlsOn = value; }
    }

    private bool gameStarted = false;
    private float jumpSpeed = 4.5f;
    private float playerSpeed = .5f;
    private float moveSpeed = 0.5f;
    private float distToGround;
    private GameObject mainCamera;
    private GameObject player;
    private Rigidbody playerRb;
    public Animator animator;
    private bool fixedcamera = false;
    private int heartcounter = 1;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Low Poly Warrior");
        playerRb = player.GetComponent<Rigidbody>();
        distToGround = player.GetComponent<Collider>().bounds.extents.y;
    }

    //4.347826
    // Update is called once per frame
    // Fixed jump issue by using a LateUpdate as it's only called once per frame.
    void LateUpdate()
    {
        if(gameStarted == true)
        {
            if(mainCamera.transform.position.x < 0 && fixedcamera == false)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x+0.03f, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                animator.SetBool("Started", true);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + playerSpeed);
                fixedcamera = true;

                if(heartcounter < 4)
                {
                    GameObject heart = GameObject.Find("heart" + heartcounter);
                    Animator a = heart.GetComponent<Animator>();
                    a.SetBool("StartClicked", true);

                    heartcounter++;
                }

                AudioController.Instance.PlayLoopAudio("running_ground");
            }
            
            // Check if player wants to jump, and if player is on the ground.
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && controlsOn)
            {
                PlayerJump();
            }

            // Check if player wants to move left
            if (Input.GetKeyDown(KeyCode.LeftArrow) && controlsOn)
            {
                MoveLeft();
            }

            // Check if player wants to move right
            if (Input.GetKeyDown(KeyCode.RightArrow) && controlsOn)
            {
                MoveRight();
            }

            if (Input.GetMouseButtonDown(0) && controlsOn)
            {
                PlayerAttack();
            }

            // Trying to smooth out movement, will look into this further.
            //player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position, 0.5f * Time.deltaTime);
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
        playerRb.AddForce(new Vector3(0.0f, 1.6f, 0.0f) * jumpSpeed, ForceMode.Impulse);
        animator.SetTrigger("Jump");

        AudioController.Instance.PlayAudioOnce("jump");
    }

    private void MoveLeft()
    {
        if(player.transform.position.x >= -5f)
        {
            // Test attempts.
            //player.transform.Translate(new Vector3(player.transform.position.x - moveSpeed, player.transform.position.y, player.transform.position.z) * Time.deltaTime);

            //Vector3 movement = new Vector3(player.transform.position.x - moveSpeed, player.transform.position.y, player.transform.position.z);
            //playerRb.MovePosition(movement * Time.fixedDeltaTime);

            player.transform.position = new Vector3(player.transform.position.x - moveSpeed, player.transform.position.y, player.transform.position.z);
        }
    }

    private void MoveRight()
    {
        if (player.transform.position.x <= 5f)
        {
            //player.transform.Translate(new Vector3(player.transform.position.x + moveSpeed, player.transform.position.y, player.transform.position.z) * Time.deltaTime);

            player.transform.position = new Vector3(player.transform.position.x + moveSpeed, player.transform.position.y, player.transform.position.z);
        }
    }

    public void SetPlayerSpeed(float speed)
    {
        playerSpeed = speed;
    }

    private void PlayerAttack()
    {
        animator.SetTrigger("Attack");
    }

    private void OnMouseDown()
    {
        gameStarted = true;
        Debug.Log("Game Started");

        AudioController.Instance.PlayAudioOnce("horn");
    }
}
