using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ouvrir : MonoBehaviour
{
    [SerializeField] public GameObject OpenCanvas;
    [SerializeField] public GameObject CloseCanvas;
    [SerializeField] private Animator OpenDoor1;
    [SerializeField] private Animator OpenDoor2;
    
    private bool _isInTrigger;
    private bool _isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        OpenCanvas.SetActive(false);
        CloseCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isOpen && _isInTrigger)
        {
            _isOpen = true;
            OpenDoor1.SetBool("Opening", _isOpen);
            OpenDoor2.SetBool("Opening", _isOpen);
            OpenCanvas.SetActive(false);
            CloseCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isOpen && _isInTrigger)
        {
            _isOpen = false;
            OpenDoor1.SetBool("Opening", _isOpen);
            OpenDoor2.SetBool("Opening", _isOpen);
            OpenCanvas.SetActive(true);
            CloseCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isInTrigger = true;
        if (_isOpen)
        {
            CloseCanvas.SetActive(true);
        }
        else if (!_isOpen)
        {
            OpenCanvas.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        _isInTrigger = false;
        OpenCanvas.SetActive(false);
        CloseCanvas.SetActive(false);
    }
}
