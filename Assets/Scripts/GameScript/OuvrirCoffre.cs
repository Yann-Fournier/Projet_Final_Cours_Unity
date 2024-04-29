using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvrirCoffre : MonoBehaviour
{
    [SerializeField] public GameObject OpenCanvas;
    [SerializeField] public Animator OpenCoffre;

    private bool _isInTrigger;
    private bool _isOpen;

    // Update is called once per frame
    void Update()
    {
        if (!_isOpen && _isInTrigger)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _isOpen = true;
                OpenCoffre.SetBool("isOpen", _isOpen);
                OpenCanvas.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isOpen)
        {
            OpenCanvas.SetActive(true);
        }
        _isInTrigger = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        OpenCanvas.SetActive(false);
        _isInTrigger = false;
    }
}
