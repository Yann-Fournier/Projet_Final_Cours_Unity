using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinBehevior : MonoBehaviour
{
    
    [SerializeField] Animator GoblinAnimator;
    [SerializeField] GameObject GoblinWeapon;
    
    private Rigidbody _rb;
    private Transform _t;
    private CapsuleCollider _cc;
    private SphereCollider _sc;
    private NavMeshAgent _n;
    private Vector3 _startPosition;
    private bool _canAttack;
    private bool _isDead;
    private bool _navMeshEnable = true;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _cc = GetComponent<CapsuleCollider>();
        _sc = GetComponent<SphereCollider>();
        _n = GetComponent<NavMeshAgent>();
        _startPosition = _t.GetComponentInParent<Transform>().position;
    }

    private void Update()
    {
        if (_isDead)
        {
            GoblinWeapon.gameObject.tag = "Untagged";
        }
        print(GoblinWeapon.gameObject.tag);
        Vector3 distanceVector = GameManager.Instance.Player.GetComponent<Transform>().position - _t.position;
        float distance = distanceVector.magnitude;
        if (distance < 1.5 && _navMeshEnable)
        {
            _canAttack = true;
            // GoblinWeapon.gameObject.tag = "EnemiesWeapon";
            StartCoroutine(SetTemporaryValue_EnemiesWeaponTag(2));
        }
        else
        {
            _canAttack = false;
            // GoblinWeapon.gameObject.tag = "Untagged";
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
            _rb.position -= new Vector3(0f, 1f, 0f);
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            _sc.enabled = false;
            _cc.enabled = false;
            _n.enabled = false;
            _navMeshEnable = false;
            Destroy(this.gameObject, 3);
        } 
    }
    
    private void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon") && GameManager.Instance.IsAttaking)
        {
            _isDead = true;
            GameManager.Instance.NumberMonsterKill++;
            _rb.position -= new Vector3(0f, 1f, 0f);
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            _sc.enabled = false;
            _cc.enabled = false;
            _n.enabled = false;
            _navMeshEnable = false;
            Destroy(this.gameObject, 3);
        } 
    }
    
    IEnumerator SetTemporaryValue_EnemiesWeaponTag(float time)
    {
        GoblinWeapon.gameObject.tag = "EnemiesWeapon";
        yield return new WaitForSeconds(time);
        GoblinWeapon.gameObject.tag = "Untagged";
    }
}
