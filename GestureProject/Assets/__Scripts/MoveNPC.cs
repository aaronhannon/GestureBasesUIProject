using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    private float moveSpeed = 4f;

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
