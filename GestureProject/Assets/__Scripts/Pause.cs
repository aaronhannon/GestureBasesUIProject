using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameObject camera;
    private Animator anim;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
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
            }
            else
            {
                paused = false;
                anim.SetBool("Paused", false);
            }
                
        }
    }
}
