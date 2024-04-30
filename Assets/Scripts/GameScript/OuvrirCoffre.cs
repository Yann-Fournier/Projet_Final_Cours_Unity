using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class OuvrirCoffre : MonoBehaviour
{
    [SerializeField] public GameObject OpenCanvas;
    [SerializeField] public GameObject PrendreCanvas;
    [SerializeField] public Animator OpenCoffre;
    [SerializeField] public GameObject Weapon;
    [SerializeField] public GameObject PlayerHand;
        
    private bool _isInTrigger;
    private bool _isOpen;

    private void Start()
    {
        Weapon.SetActive(false);
        PrendreCanvas.SetActive(false);
        OpenCanvas.SetActive(false);
    }

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
                Weapon.SetActive(true);
                PrendreCanvas.SetActive(true);
            }
        } 
        else if (_isOpen && _isInTrigger)
        {
            if (Input.GetKey(KeyCode.E))
            {
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isOpen)
        {
            OpenCanvas.SetActive(true);
        } 
        else if (_isOpen)
        {
            PrendreCanvas.SetActive(true);
        }
        _isInTrigger = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        OpenCanvas.SetActive(false);
        PrendreCanvas.SetActive(false);
        _isInTrigger = false;
    }
}
