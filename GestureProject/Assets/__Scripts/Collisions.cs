using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    private int lives = 3;
    private bool helmet = false;
    private ScoreScript scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("collided", true);

            if(helmet == false)
            {
                GameObject heart = GameObject.Find("heart" + lives);
                Animator a = heart.GetComponent<Animator>();
                a.SetBool("Destroyed", true);
                //Destroy(heart);

                lives--;

                if (lives == 0)
                {
                    scoreScript.GenerateScore();
                    scoreScript.ResetScore();

                    // Turn off controls again when player dies.
                    StartGame.ControlsOn = false;

                    SceneManager.LoadScene(0);
                    Debug.Log("GameOver");
                }
            }
            else
            {
                GameObject.Find("helmPowerUp").GetComponent<Animator>().SetBool("spawn", false);
                GameObject.Find("playerHelm").GetComponent<Animator>().SetBool("spawnHelm", false);
                helmet = false;
            }

            AudioController.Instance.PlayAudioOnce("smash_fence");
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

            other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z); ;
            scoreScript.IncreaseCoinCount();

            AudioController.Instance.PlayAudioOnce("coin");
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
}
