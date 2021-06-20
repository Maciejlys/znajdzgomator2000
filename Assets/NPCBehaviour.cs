using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NPCBehaviour : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform[] wayPoints;
    private Vector3 _currentDest;
    private States _currentState;
    private Trigger _trigger;
    enum States
    {
        Patroling,
        Chasing,
        Evade
    }

    // Start is called before the first frame update
    void Start()
    {
        _trigger = GetComponentInChildren<Trigger>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
        StartCoroutine("Move");
        _currentState = States.Patroling;
    }

    private void SetDestination()
    {
        if (_currentDest == wayPoints[0].position)
        {
            _currentDest = wayPoints[1].position;
            Debug.Log("Going to 1");
        }
        else
        {
            _currentDest = wayPoints[0].position;
            Debug.Log("Going to 0");
        }
        _navMeshAgent.SetDestination(_currentDest);
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (_currentState == States.Chasing)
            {
                _navMeshAgent.SetDestination(_trigger.GetPlayerPos());
                Debug.Log("Chasing");
                yield return new WaitForSeconds(1f);
            }
            else if (_currentState == States.Patroling)
            {
                if (Mathf.Abs(Vector3.Distance(_currentDest, transform.position)) < 1)
                {
                    SetDestination();
                }
                Debug.Log("Patroling");
                yield return new WaitForSeconds(1f);
            }
            else if (_currentState == States.Evade)
            {
                if (Mathf.Abs(Vector3.Distance(wayPoints[0].position, transform.position)) < 1)
                {
                    _currentState = States.Patroling;
                }
                Debug.Log("Evade");
                _navMeshAgent.SetDestination(wayPoints[0].position);
                _currentDest = wayPoints[0].position;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void ChangeToChase()
    {
        _currentState = States.Chasing;
    }

    public void ChangeToPatrol()
    {
        _currentState = States.Evade;
    }
}