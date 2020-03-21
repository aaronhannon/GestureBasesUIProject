using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles starting of game and enablement of player controls
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
    public float playerSpeed;
    private float moveSpeed = 2.5f;
    private float distToGround;
    private GameObject mainCamera;
    private GameObject player;
    private Rigidbody playerRb;
    private CapsuleCollider playerCollider;
    public Animator animator;
    private GameObject tutorialText;
    private bool fixedcamera = false;
    private bool isRolling = false;
    private int heartcounter = 1;
    private bool playerMovement;
    private bool onetime = false;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Low Poly Warrior");
        tutorialText = GameObject.Find("TutorialText");
        playerRb = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<CapsuleCollider>();
        SetPlayerSpeed(.3f);
        distToGround = player.GetComponent<Collider>().bounds.extents.y;
        playerMovement = true;
        //KManager.OnSwipeUpDown += new KManager.SimpleEvent(KinectManagerScript_OnSwipeUpDown);
    }
    
    //4.347826
    // Update is called once per frame
    // Fixed jump issue by using a LateUpdate as it's only called once per frame.
    void FixedUpdate()
    {
        if(gameStarted == true)
        {
            controlsOn = true;

            if(mainCamera.transform.position.x < 0 && fixedcamera == false)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x+0.03f, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                animator.SetBool("Started", true);
                if (playerMovement) {
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + playerSpeed);
                }
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
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && controlsOn && !onetime)
            {
                PlayerJump();
                // Only call method once.
                onetime = true;
            }

            // Check if player wants to move left
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && controlsOn && !onetime)
            {
                MoveLeft();
                onetime = true;
            }

            // Check if player wants to move right
            else if (Input.GetKeyDown(KeyCode.RightArrow) && controlsOn && !onetime)
            {
                MoveRight();
                onetime = true;
            }

            // Check if player is sliding.
            else if (Input.GetKeyDown(KeyCode.S) && controlsOn)
            {
                RollForward();
            }

            else if (Input.GetMouseButtonDown(0) && controlsOn)
            {
                PlayerAttack();
            }
            else
            {
                onetime = false;
            }
        }
    }
    
    // Check if player is touching the ground by sending a raycast down. 
    // If the distance is greater than .05 then it returns false E.g Player is jumping.
    public bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, distToGround + 0.5f);
    }

    // Make the player jump, and trigger animation.
    public void PlayerJump()
    {
        //Jump method only gets called when user is sprinting this prevents KManager calling method multiple times in one gesture
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sprint") && IsGrounded()){
            playerRb.AddForce(new Vector3(0.0f, 1.6f, 0.0f) * jumpSpeed, ForceMode.Impulse);
            animator.SetTrigger("Jump");

            AudioController.Instance.PlayAudioOnce("jump");
        }
    }

    public void MoveLeft()
    {
        if(player.transform.position.x >= -2f && controlsOn)
        {
            player.transform.position = new Vector3(player.transform.position.x - moveSpeed, player.transform.position.y, player.transform.position.z);
            animator.SetTrigger("RollLeft");
        }
    }

    public void MoveRight()
    {
        if (player.transform.position.x <= 2f && controlsOn)
        {
            player.transform.position = new Vector3(player.transform.position.x + moveSpeed, player.transform.position.y, player.transform.position.z);
            animator.SetTrigger("RollRight");
        }
    }
    
    public void RollForward()
    {

        if (controlsOn)
        {
            animator.SetTrigger("RollForward");
        }

        //AudioController.Instance.PlayAudioOnce("roll");
    }

    public void SetPlayerSpeed(float speed)
    {
        playerSpeed = speed;
    }

    public void PlayerAttack()
    {
        if (controlsOn)
        {
            animator.SetTrigger("Attack");
        }
        
    }

    public void OnMouseDown()
    {
        gameStarted = true;
        Debug.Log("Game Started");

        AudioController.Instance.PlayAudioOnce("horn");

        tutorialText.SetActive(false);
    }

    public void SetMovementState(bool boolVal)
    {
        playerMovement = boolVal;
    }

}
