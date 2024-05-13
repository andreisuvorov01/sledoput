using UnityEngine;

public class DoorFinalLevelTwo : MonoBehaviour
{
    private bool _isFinal = false;
    private void Update()
    {
        if (_isFinal)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0.2f, transform.position.z), Time.deltaTime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isFinal = true;
        }
    }
}
