using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
    private int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        
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

            GameObject heart = GameObject.Find("heart" + lives);
            Animator a = heart.GetComponent<Animator>();
            a.SetBool("Destroyed", true);
            //Destroy(heart);
            
            lives--;

            if (lives == 0)
            {
                SceneManager.LoadScene(0);
                Debug.Log("GameOver");
            }

            
        }
    }
}
