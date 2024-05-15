using UnityEngine;

namespace Viapix_HealingItem
{
    public class Viapix_HealingItem : MonoBehaviour
    {
        [SerializeField]
        float rotationSpeedX, rotationSpeedY, rotationSpeedZ;

        [SerializeField]
        int healingAmount;

        GameObject playerObj;

        private void Start()
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.transform.GetComponent<health>().HP < 100)
                {
                    playerObj.GetComponent<health>().HP += healingAmount;
                    Destroy(gameObject);
                }
            }
        }
    }
}

