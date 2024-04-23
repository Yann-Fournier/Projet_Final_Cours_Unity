using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkspawn : MonoBehaviour
{
    public Vector3 RespawnPosition;

    public static Vector3 LastRespawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        LastRespawnPosition = RespawnPosition;
    }
    
    // Dans le script des piques
    // private void OnTriggerEnter(Collider other)
    // {
    //     other.transform.position = Checkspawn.LastRespawnPosition;
    // }
}
