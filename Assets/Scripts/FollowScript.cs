using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{
    private NavMeshAgent _nav;

    private Transform _player;
    [SerializeField] private List<Transform> _pointPosition;
    [SerializeField] private float distance = 40f;
    public Animator animatok;
    private AudioSource _audioEnemy;

    private void Start()
    {
        _audioEnemy = GetComponent<AudioSource>();
        animatok = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        StartCoroutine(GoDistancePlayer());
        

    }
    private bool CanSeePlayer()
    {
        if (Physics.Raycast(transform.position, _player.position - transform.position, out var hitInfo) && hitInfo.transform.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }
    public IEnumerator GoDistancePlayer()
    {
        yield return null;
        if (Vector3.Distance(transform.position, _player.position) < distance && CanSeePlayer())
        {
            if (_nav.enabled) _nav.SetDestination(_player.position);
            _audioEnemy.Play();
        }
        else
        {
            if (_nav.enabled)
            {
                _audioEnemy.Play();
                if (_nav.remainingDistance <= _nav.stoppingDistance)
                {
                    if (_nav)
                        _nav.SetDestination(_pointPosition[Random.Range(0, _pointPosition.Count)].position);
                }
            }
            else
            {
                _audioEnemy.Stop();
            }
        }
    }
}
