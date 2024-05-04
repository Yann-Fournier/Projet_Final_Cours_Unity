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
    private bool _canAttack;
    private float _truc = 5000;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _c = GetComponent<CapsuleCollider>();
        _n = GetComponent<NavMeshAgent>();
        _startPosition = _t.GetComponentInParent<Transform>().position;
    }

    private void Update()
    {
        // if (_rb.velocity.magnitude * 5 > 1)
        // {
        //     print(_rb.velocity.magnitude*5);
        // }

        if (_truc != 0)
        {
            GoblinAnimator.SetFloat("Speed", 0.6f);
            GoblinAnimator.SetBool("CanAttack", true);
            _truc--;
        }
        else
        {
            Vector3 distanceVector = GameManager.Instance.Player.GetComponent<Transform>().position - _t.position;
            float distance = distanceVector.magnitude;
            if (distance < 1.5)
            {
                _canAttack = true;
            }
            else
            {
                _canAttack = false;
            }
        
            GoblinAnimator.SetFloat("Speed", _rb.velocity.magnitude * 10);
            GoblinAnimator.SetBool("CanAttack", _canAttack);
        }
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
