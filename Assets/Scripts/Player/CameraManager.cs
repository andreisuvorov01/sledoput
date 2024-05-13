using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    RaycastHit hit;
    private GameObject buttonEPanel;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
        {
            if (hit.collider.GetComponent<ChasiFinal>())
            {
                if(buttonEPanel)
                buttonEPanel.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    var numberScene = SceneManager.GetActiveScene().buildIndex;
                    if(numberScene ==2 )SceneManager.LoadScene(3);
                    else if(numberScene ==4 )SceneManager.LoadScene(5);
                }
            }
        }
    }
}
