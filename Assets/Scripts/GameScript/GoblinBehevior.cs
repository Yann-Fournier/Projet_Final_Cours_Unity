using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEngine.CoreModule;
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
    private bool _isDead;
    private bool _navMeshEnable = true;
    
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
        Vector3 distanceVector = GameManager.Instance.Player.GetComponent<Transform>().position - _t.position;
        float distance = distanceVector.magnitude;
        if (distance < 1.5 && _navMeshEnable)
        {
            _canAttack = true;
        }
        else
        {
            _canAttack = false;
        }
        
        GoblinAnimator.SetFloat("Speed", _rb.velocity.magnitude * 10);
        GoblinAnimator.SetBool("CanAttack", _canAttack);
        GoblinAnimator.SetBool("IsDead", _isDead);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && _navMeshEnable)
        {
            _n.SetDestination(GameManager.Instance.Player.GetComponent<Transform>().position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && _navMeshEnable)
        {
            _n.SetDestination(_startPosition);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon") && GameManager.Instance.IsAttaking)
        {
            _isDead = true;
            GameManager.Instance.NumberMonsterKill++;
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            this.GetComponent<SphereCollider>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
            this.GetComponent<NavMeshAgent>().enabled = false;
            _navMeshEnable = false;
            Destroy(this.gameObject, 3);
        } 
    }
    private void OnCollisionExit(Collision other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon") && GameManager.Instance.IsAttaking)
        {
            _isDead = true;
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            this.GetComponent<SphereCollider>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
            this.GetComponent<NavMeshAgent>().enabled = false;
            _navMeshEnable = false;
            Destroy(this.gameObject, 3);
        } 
    }
}
