using UnityEngine;

public class finalBoss : MonoBehaviour
{
    [SerializeField] private GameObject slime;
    [SerializeField] private GameObject chasi;
    [SerializeField] private AudioSource chasiAudio;
    private bool _isKillBoss = true;

    private void Start()
    {
        chasiAudio = GameObject.Find("Chasi").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (slime == null)
        {
            chasi.SetActive(true);
            chasiAudio.Play();
        }
    }
}
