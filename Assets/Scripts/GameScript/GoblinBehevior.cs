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
    private bool _isWaiting;
    private bool _hasWait;
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
        if (GameManager.Instance.RestartCombat)
        {
            _isDead = false;
            _canAttack = false;
            GameManager.Instance.NumberMonsterKill = 0;
            _rb.constraints = RigidbodyConstraints.None;
            _sc.enabled = true;
            _cc.enabled = true;
            _n.enabled = true;
            _navMeshEnable = true;
        }
        
        if (_isDead)
        {
            GoblinWeapon.gameObject.tag = "Untagged";
            _canAttack = false;
        }

        // print(GoblinWeapon.gameObject.tag);
        Vector3 distanceVector = GameManager.Instance.Player.GetComponent<Transform>().position - _t.position;
        float distance = distanceVector.magnitude;
        if (distance < 1.5 && _navMeshEnable)
        {
            StartCoroutine(SetTemporaryValue_isWaiting(1.5f));
            _hasWait = true;
        }

        if (!_isWaiting && _hasWait)
        {
            StartCoroutine(SetTemporaryValue_EnemiesWeaponTag(1.5f));
            _hasWait = false;
        }

        if (_canAttack)
        {
            GoblinWeapon.gameObject.tag = "EnemiesWeapon";
        }
        else
        {
            GoblinWeapon.gameObject.tag = "Untagged";
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
            _sc.enabled = false;
            _cc.enabled = false;
            _n.enabled = false;
            _navMeshEnable = false;
            // Destroy(this.gameObject, 3);
            StartCoroutine(SetTemporaryValue_EnemiesDisapear(3));
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon") && GameManager.Instance.IsAttaking)
        {
            _isDead = true;
            GameManager.Instance.NumberMonsterKill++;
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            _sc.enabled = false;
            _cc.enabled = false;
            _n.enabled = false;
            _navMeshEnable = false;
            // Destroy(this.gameObject, 3);
            StartCoroutine(SetTemporaryValue_EnemiesDisapear(3));
        }
    }
    
    IEnumerator SetTemporaryValue_EnemiesDisapear(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }


    IEnumerator SetTemporaryValue_EnemiesWeaponTag(float time)
    {
        _canAttack = true;
        yield return new WaitForSeconds(time);
        _canAttack = false;
    }

    IEnumerator SetTemporaryValue_isWaiting(float time)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(time);
        _isWaiting = false;
    }
}
