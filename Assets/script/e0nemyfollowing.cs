using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Transform _player;
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
    _player = GameObject.FindGameObjectWithTag("Player").transform;
}

void Update()
{
    if (CanSeePlayer())
    {
        _nav.SetDestination(_player.position);
    }
}

bool CanSeePlayer()
{
    // Добавьте здесь логику проверки видимости игрока
    // Например, можно использовать Raycast или другие методы

    // Пример проверки с помощью Raycast:
    RaycastHit hit;
    if (Physics.Raycast(transform.position, _player.position - transform.position, out hit))
    {
        if (hit.transform.CompareTag("Player"))
        {
            return true;
        }
    }

    return false;
}
}

