using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            print("Collide found");
            print(gameObject.GetComponent<Animator>().GetBool("pickup"));
            gameObject.GetComponent<Animator>().SetBool("pickup", true);
            print(gameObject.GetComponent<Animator>().GetBool("pickup"));
        }
    }
}
