using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class for Weapon to kill enemies
public class WeaponController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if weapon collides with enemy, then destroy them
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
