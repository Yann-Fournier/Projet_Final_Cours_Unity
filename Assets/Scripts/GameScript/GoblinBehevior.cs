using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinBehevior : MonoBehaviour
{
    
    [SerializeField] Animator GoblinAnimator;

    private Rigidbody _rb;
    private Transform _t;
    private CapsuleCollider _c;
    private NavMeshAgent _n;
    private Vector3 _startPosition;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _c = GetComponent<CapsuleCollider>();
        _n = GetComponent<NavMeshAgent>();
        _startPosition = _t.GetComponentInParent<Transform>().position;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _n.SetDestination(GameManager.Instance.Player.GetComponent<Transform>().position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _n.SetDestination(_startPosition);
        }
    }
}
