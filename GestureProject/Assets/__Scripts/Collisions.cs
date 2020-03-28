using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class that handles all issues surrounding player and object collisions
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
    private GameObject player;
    private Pause pause;

    // Start is called before the first frame update
    void Start()
    {
        // Find Gameobjects and scripts from UI.
        scoreScript = FindObjectOfType<ScoreScript>();
        startgame = FindObjectOfType<StartGame>();
        playerRb = GameObject.Find("Low Poly Warrior").GetComponent<Rigidbody>();
        playerAnimator = GameObject.Find("Low Poly Warrior").GetComponent<Animator>();
        player = GameObject.Find("Low Poly Warrior");
        pause = GameObject.Find("Main Camera").GetComponent<Pause>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if player is in boat and game isn't paused.
        if (inboat && (pause.GetPauseValue()==false))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
            GameObject.Find("boat").transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
            startgame.SetPlayerSpeed(.8f);
        }
    }

    // Method to allow player to jump
    public void PlayerJump()
    {
        playerRb.AddForce(new Vector3(0.0f, 1.6f, 0.0f) * jumpSpeed, ForceMode.Impulse);
    }

    // Called when a collider colliders with another collider, e.g. player with obstacle.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle") || other.CompareTag("VillageObstacle") || other.CompareTag("ForestObstacle")|| other.CompareTag("RiverObstacle"))
        {
            // Check if object is tree by name so animation can be triggered.
            if (!other.gameObject.name.Contains("tree")) {
                other.gameObject.GetComponent<Animator>().SetBool("collided", true);
            }

            // Check if player has helmet, else remove one life.
            TakeDamage(other);

            AudioController.Instance.PlayAudioOnce("smash_fence");
        }
        else if(other.CompareTag("EndChunk")){
            // If last chunk, destroy and generate a new chunk.
            Destroy(other.transform.parent.gameObject);
            startgame.GetComponent<GenerateChunks>().GenerateChunk();
        }
        else if (other.CompareTag("boat"))
        {
            // If not in boat, play animation to jump in boat.
            if(inboat == false)
            {
                gameObject.GetComponent<Animator>().SetTrigger("inboat");
            }
        }
        else if (other.CompareTag("boatPre"))
        {
            gameObject.GetComponent<Animator>().SetBool("outboat", false);
            inboat = true;
            gameObject.GetComponent<Animator>().SetBool("stopRun",true);
        }
        else if (other.CompareTag("water"))
        {
            if (inboat == false)
            {
                //need to kill player if in water and not in boat, so reset all collectable and kill player
                helmet = false;
                revive = false;
                reviveDisplay.SetActive(false);
                lives = 1;
                TakeDamage(other);
            }
        }
        else if (other.CompareTag("outboat"))
        {
            inboat = false;
            gameObject.GetComponent<Animator>().SetBool("outboat", true);
            gameObject.GetComponent<Animator>().SetBool("stopRun", false);
            Destroy(GameObject.Find("boat"));
            startgame.SetPlayerSpeed(.4f);
        }
        else if (other.CompareTag("Enemy"))
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
                TakeDamage(other);
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
                TakeDamage(other);
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
            // Collect helmet object.
            GameObject.Find("playerHelm").GetComponent<Animator>().SetBool("spawnHelm",true);
            GameObject.Find("helmPowerUp").GetComponent<Animator>().SetBool("spawn", true);
            helmet = true;

            AudioController.Instance.PlayAudioOnce("pickup_1");
        }
        else if (other.CompareTag("coin"))
        {
            // Collect coin object.
            other.gameObject.GetComponent<Animator>().SetBool("pickup", true);

            other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
            scoreScript.IncreaseCoinCount();

            AudioController.Instance.PlayAudioOnce("coin");
        }
        else if (other.CompareTag("revive"))
        {
            // Collect revive object.
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
                StartGame.ControlsOn = true;
            }
            else
            {
                StartGame.ControlsOn = false;
            }
        }
        else if (other.CompareTag("NPCStart"))
        {
            // Finds all NPC's available and starts them running.
            // This is so the NPC haven't run off screen until you get to the required chunk.
            var foundNPCs = FindObjectsOfType<MoveNPC>();

            foreach (var item in foundNPCs)
            {
                item.MoveSpeed = 4f;
            }
        }
    }

    // Check if player has helmet, else remove one life.
    private void TakeDamage(Collider collisionItem)
    {
        if (helmet == false)
        {
            GameObject heart = GameObject.Find("heart" + lives);
            Animator a = heart.GetComponent<Animator>();
            a.SetBool("Destroyed", true);

            lives--;

            if (lives == 0)
            {
                ResetGame(collisionItem);
            }
        }
        else
        {
            GameObject.Find("helmPowerUp").GetComponent<Animator>().SetBool("spawn", false);
            GameObject.Find("playerHelm").GetComponent<Animator>().SetBool("spawnHelm", false);
            helmet = false;
        }
    }

    //method to reset the game
    private void ResetGame(Collider collisionItem) {
        //play death animation
        playerAnimator.SetTrigger("Death");

        //destroy collision item to avoid extra hearts damage upon revival if user has one
        Destroy(collisionItem);

        // Turn off controls again when player dies.
        StartGame.ControlsOn = false;

        // if user has collected a revive potion
        if (revive)
        {
            reviveDisplay.GetComponent<Animator>().SetTrigger("UseRevive");
            startgame.SetMovementState(false);

            //trigger revive animation transition
            playerAnimator.SetTrigger("Revive");
            AudioController.Instance.PlayAudioOnce("GlassBreak");
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
            Invoke("SetReviveDisplayInactive", 0.3f);
            Invoke("SetMovementStateTrue", 2);
        }
        else {
            //play death sound
            AudioController.Instance.PlayAudioOnce("playerDeath");
            //set the player movement state to false
            startgame.SetMovementState(false);

            //generate score using coins collected and reset score
            scoreScript.GenerateFinalScore();
            scoreScript.ResetScore();

            //after 1 second - restart game (allow death animation to be seen)
            GameObject.Find("Image").GetComponent<Animator>().SetTrigger("gameover");
            Invoke("GameOver", 2);
        }
    }

    //set movement state to true
    private void SetMovementStateTrue()
    {
        startgame.SetMovementState(true);
    }

    //set revive display to false
    private void SetReviveDisplayInactive()
    {
        reviveDisplay.SetActive(false);
    }

    //game has finished, load the death scene
    private void GameOver()
    {
        //load death scene
        SceneManager.LoadScene(1);
    }
}
