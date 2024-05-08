using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    //Для показа и скрытия бара
    public bool showBar;
    //Ширина бара
    public float barWidth;
    //Высота бара
    public float barHeight;
    //Хп, которое будет отображаться в баре
    public int health;
    public int healthMax;

    // Use this for initialization
    void Start()
    {
        //Скрываем бар при старте
        showBar = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //Если бар показывается
        if (showBar)
        {
            //Создаём строку, которая будет отображаться в 2 вариантах
            string str;
            if (health > 0) { str = health + " / " + healthMax; }
            else { str = "Dead"; }
            //Рисуем бар
            GUI.Box(
                    new Rect(Screen.width / 2 - barWidth / 2, barHeight, barWidth, barHeight),
                    str);
        }
    }
}