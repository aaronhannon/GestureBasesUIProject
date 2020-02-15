using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0f, 1f, 0f);
    private float moveSpeed = 4f;
    
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
            //transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
