using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiliatinaRotation : MonoBehaviour
{
    public float swingSpeed = 1.0f; 
    public float swingAngle = 10.0f; 
    private float startAngle;
    [SerializeField] private int damage;

    void Start()
    {
        startAngle = transform.localEulerAngles.z;
    }
    void Update()
    {
        float angle = startAngle + swingAngle * Mathf.Sin(Time.time * swingSpeed); 
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.transform.GetComponent<health>().TakeDamage(damage);
        }
    }
}
    