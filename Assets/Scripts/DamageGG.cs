using UnityEngine;

public class DamageGG : MonoBehaviour
{
	public int damageAmount;
    public FollowScript follow;
    private AudioSource _audioEnemy;
    [SerializeField] private AudioClip _audiokusiEnemy;


    private void Start()
    {
        follow = GetComponent<FollowScript>();
        _audioEnemy = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
            _audioEnemy.clip = _audiokusiEnemy;
            _audioEnemy.Play();
            other.GetComponent<health>().TakeDamage(damageAmount);
            follow.animatok.SetInteger("move", 2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            follow.animatok.SetInteger("move", 0);
        }
    }
}
