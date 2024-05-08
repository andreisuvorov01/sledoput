using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    //��� ������ � ������� ����
    public bool showBar;
    //������ ����
    public float barWidth;
    //������ ����
    public float barHeight;
    //��, ������� ����� ������������ � ����
    public int health;
    public int healthMax;

    // Use this for initialization
    void Start()
    {
        //�������� ��� ��� ������
        showBar = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //���� ��� ������������
        if (showBar)
        {
            //������ ������, ������� ����� ������������ � 2 ���������
            string str;
            if (health > 0) { str = health + " / " + healthMax; }
            else { str = "Dead"; }
            //������ ���
            GUI.Box(
                    new Rect(Screen.width / 2 - barWidth / 2, barHeight, barWidth, barHeight),
                    str);
        }
    }
}