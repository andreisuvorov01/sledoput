using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{
	private NavMeshAgent _nav;

	private Transform _player;

	private void Start()
	{
		_nav = GetComponent<NavMeshAgent>();
		_player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		if (CanSeePlayer())
		{
			_nav.SetDestination(_player.position);
		}
	}

	private bool CanSeePlayer()
	{
		if (Physics.Raycast(base.transform.position, _player.position - base.transform.position, out var hitInfo) && hitInfo.transform.CompareTag("Player"))
		{
			return true;
		}
		return false;
	}
}
