using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Damaging : MonoBehaviour
{
    public GameObject PrefabFire;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Untagged")
        {
            print(other.tag);
        }
        
        if (GameManager.Instance.IsAttaking)
        {
            if (other.tag == "Baril")
            {
                GameObject fire = Instantiate(PrefabFire);
                fire.GetComponent<Transform>().position = other.transform.position;
                Destroy(other.gameObject);
                Destroy(fire.gameObject, 7);
            }

            if (other.tag == "Enemies")
            {
                GameManager.Instance.NumberMonsterKill++;
                other.GetComponent<SphereCollider>().enabled = false;
                other.GetComponent<CapsuleCollider>().enabled = false;
                other.GetComponent<NavMeshAgent>().enabled = false;
                Destroy(other.gameObject, 1);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Untagged")
        {
            print(other.tag);
        }
        
        if (GameManager.Instance.IsAttaking)
        {
            if (other.tag == "Baril")
            {
                GameObject fire = Instantiate(PrefabFire);
                fire.GetComponent<Transform>().position = other.transform.position;
                Destroy(other.gameObject);
                Destroy(fire.gameObject, 7);
            }
            
            if (other.tag == "Enemies")
            {
                GameManager.Instance.NumberMonsterKill++;
                other.GetComponent<SphereCollider>().enabled = false;
                other.GetComponent<CapsuleCollider>().enabled = false;
                other.GetComponent<NavMeshAgent>().enabled = false;
                Destroy(other.gameObject, 1);
            }
            
            if (other.tag == "test")
            {
                print("test");
            }
        }
    }
}
