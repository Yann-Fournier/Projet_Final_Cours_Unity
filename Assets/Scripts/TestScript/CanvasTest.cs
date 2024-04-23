using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasTest : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }
}
