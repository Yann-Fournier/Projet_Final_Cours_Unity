using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehevior : MonoBehaviour
{
    [SerializeField] float Speed;
    
    [SerializeField] Animator GoblinAnimator;

    private Rigidbody _rb;
    private Transform _t;
    private CapsuleCollider _c;
    private UnityEngine.AI.NavMeshAgent _n;
    private Vector3 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _c = GetComponent<CapsuleCollider>();
        _n = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _startPosition = _t.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _n.SetDestination(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _n.SetDestination(_startPosition);
        }
    }
}
