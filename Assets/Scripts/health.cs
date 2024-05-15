using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public int HP = 100;

    public Slider healthBar;
    [SerializeField] private AudioSource healthSource;

    private void Update() => healthBar.value = HP;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (healthBar.value <= 0f)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("EndGame");
            Debug.Log("LoadScene");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (HP < 100)
        {
            if (other.gameObject.tag == "Health")
            {
                healthSource.Play();
            }
        }
    }
}
