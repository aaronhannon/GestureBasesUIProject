using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle the day and night cycle in game
public class AmbientLight : MonoBehaviour
{
    private float offset = 0.001f;
    private float offsetDiv;
    private Light light;
    private bool change = false;
    private GameObject skybox;
    private MeshRenderer mr;
    private GameObject firefly;

    void Start()
    {
        // Find gameojects in UI, and set initial values.
        firefly = GameObject.Find("firefly");
        skybox = GameObject.Find("SkyDome");
        mr = skybox.GetComponent<MeshRenderer>();
        light = gameObject.GetComponent<Light>();
        light.intensity = 1.0f;
        RenderSettings.ambientIntensity = 0.5f;
        offsetDiv = offset / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //day time - light doesn't need to be on until light intensity changes
        if (change == false)
        {
            firefly.GetComponent<Animator>().SetBool("lightOff", false);
            light.intensity -= offset;
            RenderSettings.ambientIntensity -= offsetDiv;
            mr.material.mainTextureOffset = new Vector2(mr.material.mainTextureOffset.x + offsetDiv, 0);

            if (light.intensity <= 0.0f)
            {
                change = true;
            }
            if (light.intensity <= 0.25f)
            {
                firefly.GetComponent<Animator>().SetBool("lightOn",true);
            }
        }
        //night time - light needs to be on until light intensity changes
        else
        {
            firefly.GetComponent<Animator>().SetBool("lightOn", false);
            light.intensity += offset;
            RenderSettings.ambientIntensity += offsetDiv;
            mr.material.mainTextureOffset = new Vector2(mr.material.mainTextureOffset.x - offsetDiv, 0);

            if (light.intensity >= 1.0f)
            {
                change = false;
            }
            if (light.intensity >= 0.25f)
            {
                firefly.GetComponent<Animator>().SetBool("lightOff", true);
            }
        }
    }
}
