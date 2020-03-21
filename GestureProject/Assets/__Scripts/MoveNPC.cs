using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class for controlling NPC behaviour
public class MoveNPC : MonoBehaviour
{
    //set NPC movespeed
    private float moveSpeed = 0f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    //set NPC start position
    private Vector3 startPosition;
    public Vector3 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == startPosition)
        {
            // Destroy when gets to start
            Destroy(gameObject);
        }
        else
        {
            // Move NPC towards start.
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
    }
}
