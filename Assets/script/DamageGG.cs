using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageGG : MonoBehaviour
{
    public int damageAmount;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<health>().TakeDamage(damageAmount);
        }
    }
}
