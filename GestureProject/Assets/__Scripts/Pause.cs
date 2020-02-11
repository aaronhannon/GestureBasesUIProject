using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameObject camera;
    private Animator anim;
    private GameObject startbutton;
    private bool paused = false;
    private bool timer = false;
    private float timeleft = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        startbutton = GameObject.Find("StartFBX");
        camera = GameObject.Find("Main Camera");
        anim = camera.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(paused == false) {
                paused = true;
                anim.SetBool("Paused", true);
                startbutton.GetComponent<StartGame>().SetPlayerSpeed(0.0f);
            }
            else
            {
                paused = false;
                anim.SetBool("Paused", false);
                timer = true;
                
            }
                
        }

        if(timer == true)
        {
            timeleft -= Time.deltaTime;
            Debug.Log("TIMELEFT:" + timeleft);
            if (timeleft < 0)
            {
                startbutton.GetComponent<StartGame>().SetPlayerSpeed(0.2f);
                timeleft = 3.0f;
                timer = false;
            }
        }

    }
}
