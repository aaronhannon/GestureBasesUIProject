using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that handles pause behaviour in game
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
        //initialise necessary game objects and animator
        startbutton = GameObject.Find("StartFBX");
        camera = GameObject.Find("Main Camera");
        anim = camera.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if user presses escape key
        if (Input.GetKeyDown("escape"))
        {
            //call pause game method
            PauseGame();
        }

        //add timer before continueing game after an unpause
        if(timer == true)
        {
            timeleft -= Time.deltaTime;
            if (timeleft < 0)
            {
                startbutton.GetComponent<StartGame>().SetPlayerSpeed(0.3f);
                timeleft = 3.0f;
                timer = false;
            }
            var foundNPCs = FindObjectsOfType<MoveNPC>();
            // reset NPC movespeed
            foreach (var item in foundNPCs)
            {
                item.MoveSpeed = 4f;
            }
        }

    }

    //method to pause game
    public void PauseGame()
    {
        //if game isnt paused, then pause game
        if (paused == false)
        {
            //set pause to true and call pause animation
            paused = true;
            anim.SetBool("Paused", true);

            //set player and NPC's that are found in games movement speed to zero
            startbutton.GetComponent<StartGame>().SetPlayerSpeed(0.0f);
            var foundNPCs = FindObjectsOfType<MoveNPC>();
            foreach (var item in foundNPCs)
            {
                item.MoveSpeed = 0.0f;
            }
        }
        // if already paused, then unpause game
        else
        {
            paused = false;
            anim.SetBool("Paused", false);
            timer = true;
        }
    }

    //method to pause game
    public bool GetPauseValue()
    {
        return paused;
    }
}
