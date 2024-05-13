using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public schotchik schotchik_script;
    public int HP = 100;
    public FollowScript follow;
    public Slider healthBar;
    [SerializeField] private OpenDoor _door;
    private AudioSource _audioEnemy;
    [SerializeField] private AudioClip _audioGoEnemy;
    [SerializeField] private AudioClip _audiodieEnemy;
    [SerializeField] private AudioClip _audioHitEnemy;



    private void Start()
    {
        _audioEnemy = GetComponent<AudioSource>();
        //_audioEnemy.clip = _audioGoEnemy;
        //_audioEnemy.Play();
        follow = GetComponent<FollowScript>();
        schotchik_script = GameObject.Find("First Person Controller").GetComponent<schotchik>();
    }
    private void Update()
    {
        healthBar.value = HP;
        if (HP <= 0)
        {
            _audioEnemy.clip = _audiodieEnemy;
            _audioEnemy.Play();
            follow.animatok.SetInteger("move", 4);
            schotchik_script.Schotchik_value++;
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            healthBar.gameObject.SetActive(false);
            StartCoroutine(DestroyObject());
        }
    }
    public void TakeDamage(int damageAmount)
    {
        _audioEnemy.clip = _audioHitEnemy;
        _audioEnemy.Play();
        HP -= damageAmount;
        follow.animatok.SetInteger("move", 3);
        Invoke("ResetAnim", 0.2f);
    }
    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3f);
        if (_door)
        {
            _door.currentSmile += 1;
            _door.slime.Remove(gameObject);
        }
        Destroy(transform.gameObject);
    }
    private void ResetAnim() => follow.animatok.SetInteger("move", 0);


}
