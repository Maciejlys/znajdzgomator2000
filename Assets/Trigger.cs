using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private NPCBehaviour _npcBehaviour;
    private Vector3 playerPos;

    private void Start()
    {
        _npcBehaviour = GetComponentInParent<NPCBehaviour>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _npcBehaviour.ChangeToChase();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _npcBehaviour.ChangeToPatrol();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPos = other.transform.position;
        }
    }

    public Vector3 GetPlayerPos()
    {
        return playerPos;
    }
}