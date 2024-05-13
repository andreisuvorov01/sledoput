using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteniLovushkaCollider : MonoBehaviour
{
    public bool _isMoveSteni = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isMoveSteni = true;
        }
    }
}
