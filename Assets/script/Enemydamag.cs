using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemydamag : MonoBehaviour
{
    public float health;
    public Slider sliderPlayer;
    public GameObject DiePanel;

    void Update()
    {
        sliderPlayer.value = health;
    }

    public void TakeDamage(float damageAmount)
    {
        health-=damageAmount;

        if(health <=0)
        {
        GetComponent<Collider>().enabled = false;
        sliderPlayer.gameObject.SetActive(false);
        Destroy(gameObject);
        DiePanel.SetActive(true);
        }
    }
}
