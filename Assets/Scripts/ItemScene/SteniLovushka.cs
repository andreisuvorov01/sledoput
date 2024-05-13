using UnityEngine;

public class SteniLovushka : MonoBehaviour
{
    [SerializeField] private byte _steni;
    [SerializeField] private SteniLovushkaCollider _collider;
    void Update()
    {
        if (_collider._isMoveSteni)
        {
            if (_steni == 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, -227), Time.deltaTime / 5);
            }
            if (_steni == 1)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, -223), Time.deltaTime / 5);
            }
        }
    }
}
