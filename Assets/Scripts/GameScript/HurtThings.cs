using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            print(other.gameObject.name);
            if (other.transform.tag == "Enemies" && GameManager.Instance.IsAttaking)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
