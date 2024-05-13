using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Vector3 vectorEnd;
    public List<GameObject> slime;
    private float lenghtSmile;
    public float currentSmile;
    [SerializeField] private TMP_Text lenghtSmileText;

    private void Start()
    {
        vectorEnd = new Vector3(transform.position.x, transform.position.y - 13, transform.position.z);
        lenghtSmile = slime.Count;
    }
    private void Update()
    {
        if(lenghtSmileText) lenghtSmileText.text = currentSmile + "/" + lenghtSmile +"  монстров убито";
        for (int i = 0; i <= slime.Count; i++)
        {
            if (slime.Count == 0)
            {
                transform.GetComponent<Collider>().enabled = false;
                transform.position = Vector3.Lerp(transform.position, vectorEnd, Time.deltaTime * 3);
                Invoke("Destroy", 5);
            }
        }
    }
    private void Destroy() => Destroy(gameObject);



}
