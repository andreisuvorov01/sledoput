using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletLife = 1f;

    private void Awake()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
