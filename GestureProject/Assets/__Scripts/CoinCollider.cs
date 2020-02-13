using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    //public float coinY;

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            print("Collide found");
            print(gameObject.GetComponent<Animator>().GetBool("pickup"));
            gameObject.GetComponent<Animator>().SetBool("pickup", true);
            print(gameObject.GetComponent<Animator>().GetBool("pickup"));

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z); ;

        }
    }
}
