using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class health : MonoBehaviour
{
    private int HP = 100;
    public Slider healthBar;

    private void Update()
    {
        healthBar.value = HP;
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (healthBar.value <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("EndGame");
            Debug.Log("LoadScene");

        }
        
    }
}
