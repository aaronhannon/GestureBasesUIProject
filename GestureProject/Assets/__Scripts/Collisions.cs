using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    private int lives = 3;
    private bool helmet = false;
    private float jumpSpeed = 2.5f;
    private ScoreScript scoreScript;
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private bool inboat = false;
    private bool revive = false;
    public GameObject reviveDisplay;
    private StartGame startgame;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
        startgame = FindObjectOfType<StartGame>();
        playerRb = GameObject.Find("Low Poly Warrior").GetComponent<Rigidbody>();
        playerAnimator = GameObject.Find("Low Poly Warrior").GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (inboat)
        {
            //Debug.Log("BOAT " + GameObject.Find("boat").transform.position.x + " " + GameObject.Find("boat").transform.position.y + " " + GameObject.Find("boat").transform.position.z);
            //Debug.Log("Player " + gameObject.transform.position.x + " " + gameObject.transform.position.y + " " + gameObject.transform.position.z);

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
            GameObject.Find("boat").transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        }
    }

    public void PlayerJump()
    {
        playerRb.AddForce(new Vector3(0.0f, 1.6f, 0.0f) * jumpSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("collided", true);

            // Check if player has helmet, else remove one life.
            CheckForHelmet();

            AudioController.Instance.PlayAudioOnce("smash_fence");
        }
        
        if(other.CompareTag("boat"))
        {
            Debug.Log("BOAT!!");
            gameObject.GetComponent<Animator>().SetTrigger("inboat");

            //PlayerJump();
            


        }

        if (other.CompareTag("boatPre"))
        {
            inboat = true;
            //GameObject.Find("StartFBX").GetComponent<StartGame>().SetPlayerSpeed(0.0f);
            gameObject.GetComponent<Animator>().SetBool("stopRun",true);
            ////GameObject.Find("boat").transform.parent = gameObject.transform;
            //GameObject.Find("StartFBX").GetComponent<StartGame>().SetPlayerSpeed(0.5f);
            ////playerRb.useGravity = false;
        }

        if (other.CompareTag("Enemy"))
        {
            // Check if attack animation is playing.
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                // Kill NPC
                other.gameObject.GetComponent<Animator>().SetTrigger("Death");
                AudioController.Instance.PlayAudioOnce("death_1");
            }
            else
            {
                //player takes damage then add damage audio for hit - 1 life and take hit plays death sound
                if (lives > 1)
                {
                    AudioController.Instance.PlayAudioOnce("playerDamage");
                }
                
                // Check if player has helmet, else remove one life.
                CheckForHelmet();
            }
        }
        else if (other.CompareTag("roll"))
        {
            // Check if roll animation is playing.
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("RollForward"))
            {
                //player takes damage then add damage audio for hit - 1 life and take hit plays death sound
                if (lives > 1)
                {
                    AudioController.Instance.PlayAudioOnce("playerDamage");
                }

                // Check if player has helmet, else remove one life.
                CheckForHelmet();
            }
        }
        else if (other.CompareTag("trigger"))
        {
            // Get child of collider E.g tree, trigger animation and play sound.
            GameObject tree = other.transform.GetChild(0).gameObject;

            tree.GetComponent<Animator>().SetTrigger("TreeFall");

            AudioController.Instance.PlayAudioOnce("falling_tree");
        }
        else if (other.CompareTag("helmet"))
        {
            // X size = 0.003000001 y size = 0.004000003 z size = 0.003600002
            GameObject.Find("playerHelm").GetComponent<Animator>().SetBool("spawnHelm",true);
            GameObject.Find("helmPowerUp").GetComponent<Animator>().SetBool("spawn", true);
            helmet = true;

            AudioController.Instance.PlayAudioOnce("pickup_1");
        }
        else if (other.CompareTag("coin"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("pickup", true);

            other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
            scoreScript.IncreaseCoinCount();

            AudioController.Instance.PlayAudioOnce("coin");
        }
        else if (other.CompareTag("revive"))
        {
            Destroy(other);
            revive = true;
            reviveDisplay.SetActive(true);

            AudioController.Instance.PlayAudioOnce("ReviveCollect");
        }
        else if (other.CompareTag("controlsOn"))
        {
            // Turn on/off controls.
            if (StartGame.ControlsOn == false)
            {
                Debug.Log("Controls on");
                StartGame.ControlsOn = true;
            }
            else
            {
                Debug.Log("Controls off");
                StartGame.ControlsOn = false;
            }
        }
    }

    // Check if player has helmet, else remove one life.
    private void CheckForHelmet()
    {
        if (helmet == false)
        {
            GameObject heart = GameObject.Find("heart" + lives);
            Animator a = heart.GetComponent<Animator>();
            a.SetBool("Destroyed", true);
            //Destroy(heart);Destroy(heart);

            lives--;

            if (lives == 0)
            {
                ResetGame();
            }
        }
        else
        {
            GameObject.Find("helmPowerUp").GetComponent<Animator>().SetBool("spawn", false);
            GameObject.Find("playerHelm").GetComponent<Animator>().SetBool("spawnHelm", false);
            helmet = false;
        }
    }

    private void ResetGame() {
        playerAnimator.SetTrigger("Death");


        // Turn off controls again when player dies.
        StartGame.ControlsOn = false;

        AudioController.Instance.PlayAudioOnce("playerDeath");

        // if user has collected a revive potion
        if (revive)
        {
            startgame.SetMovementState(false);
            //trigger revive animation transition
            playerAnimator.SetTrigger("Revive");
            //reset lives to one
            lives = 1;

            //Reset last heart to match new lives
            GameObject heart = GameObject.Find("heart" + lives);
            Animator a = heart.GetComponent<Animator>();
            a.SetBool("Destroyed", false);

            // Turn off controls again when player dies.
            StartGame.ControlsOn = true;
            //revive used - set false
            revive = false;
            reviveDisplay.SetActive(false);
            Invoke("SetMovementStateTrue", 2);
        }
        else {
            startgame.SetMovementState(false);
            //generate score using coins collected and reset score
            scoreScript.GenerateFinalScore();
            scoreScript.ResetScore();

            //after 1 second - restart game (allow death animation to be seen)
            Invoke("GameRestart", 1);
        }
    }

    private void SetMovementStateTrue()
    {
        startgame.SetMovementState(true);
    }

    private void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
}
