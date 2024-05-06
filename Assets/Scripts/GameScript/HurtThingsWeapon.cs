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
        if (GameManager.Instance.IsAttaking)
        {
            if (other.tag == "Baril")
            {
                GameObject fire = Instantiate(PrefabFire);
                fire.GetComponent<Transform>().position = other.transform.position;
                Destroy(other.gameObject);
                Destroy(fire.gameObject, 7);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.IsAttaking)
        {
            if (other.tag == "Baril")
            {
                GameObject fire = Instantiate(PrefabFire);
                fire.GetComponent<Transform>().position = other.transform.position;
                Destroy(other.gameObject);
                Destroy(fire.gameObject, 7);
            }
        }
    }
}
