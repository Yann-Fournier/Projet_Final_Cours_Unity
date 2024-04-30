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
        
    private bool _isInTrigger;
    private bool _isOpen;
    private bool _hasWeapon;
    private GameObject _weapon;
    private GameObject _playerHand;
    private GameObject _weaponInChess;
    

    private void Start()
    {
        Weapon.SetActive(false);
        PrendreCanvas.SetActive(false);
        OpenCanvas.SetActive(false);
        _weapon = Weapon;
        _weaponInChess = _weapon.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isOpen && _isInTrigger)
        {
            if (Input.GetKeyUp(KeyCode.E))
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
            if (Input.GetKeyUp(KeyCode.E) && _weapon.transform.childCount > 0)
            {
                if (_hasWeapon)
                {
                    SwitchWeapon();
                }
                else
                {
                    TakeWeapon();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerHand = GameManager.Instance.PlayerHand;
        
        if (_playerHand.transform.childCount > 0)
        {
            _hasWeapon = true;
        }
        else
        {
            _hasWeapon = false;
        }
        
        if (!_isOpen)
        {
            OpenCanvas.SetActive(true);
        } 
        else if (_isOpen && _weapon.transform.childCount > 0)
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

    private void TakeWeapon()
    {
        // On récupère la nouvelle arme
        GameObject newChild = Instantiate(_weaponInChess);
        Destroy(_weaponInChess);
        // On l'ajoute à la main du joueur
        newChild.transform.name = ChangeString(newChild.transform.name); // On change son nom sinon ca fait des pb.
        newChild.transform.parent = GameManager.Instance.PlayerHand.transform;
        // On met à jour sa transform
        newChild.transform.localPosition = Vector3.zero;
        newChild.transform.localRotation = Quaternion.identity;
        newChild.transform.localScale = Vector3.one;

    }

    private void SwitchWeapon()
    {
        // On récupère la nouvelle arme pour faire un échange
        GameObject oldChild = Instantiate(GameManager.Instance.PlayerHand.transform.GetChild(0).gameObject);
        Destroy(GameManager.Instance.PlayerHand.transform.GetChild(0).gameObject);
        // On récupère la nouvelle arme
        GameObject newChild = Instantiate(_weaponInChess);
        Destroy(_weaponInChess);
        // On l'ajoute à la main du joueur
        newChild.transform.name = ChangeString(newChild.transform.name); // On change son nom sinon ca fait des pb.
        newChild.transform.parent = GameManager.Instance.PlayerHand.transform;
        // On met à jour sa transform
        newChild.transform.localPosition = Vector3.zero;
        newChild.transform.localRotation = Quaternion.identity;
        newChild.transform.localScale = Vector3.one;
        // On ajoute l'ancienne arme au coffre
        oldChild.transform.name = ChangeString(oldChild.transform.name);
        oldChild.transform.parent = _weapon.transform;
        _weaponInChess = oldChild;
        // On met à jour sa transform
        oldChild.transform.localPosition = new Vector3(-0.867f, 0.097f, 0f);
        oldChild.transform.localRotation = new Quaternion(-101.168f, -90f, 0f, 0f);
        oldChild.transform.localScale = Vector3.one;
    }

    private string ChangeString(string str)
    {
        int newLength = str.Length - 7; // 7 est la longeur de: (Clone).
        string newString = str.Substring(0, newLength);
        return newString;
    }
}
