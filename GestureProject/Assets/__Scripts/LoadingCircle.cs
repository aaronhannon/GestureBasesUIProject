using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to animate the rotation of the circle on a name dictation interpreter
public class LoadingCircle : MonoBehaviour
{
    private RectTransform rectComponent;
    private float rotateSpeed = 500f;

    private void Start()
    {
        //get rect transform of the circle
        rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //rotate the circle using the rotation speed
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
