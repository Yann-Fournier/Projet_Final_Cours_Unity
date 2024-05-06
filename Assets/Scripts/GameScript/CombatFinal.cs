using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFinal : MonoBehaviour
{
    [SerializeField] GameObject GrandGroupeEnemi;
    [SerializeField] GameObject PetitGroupeEnemi;
    [SerializeField] GameObject VictoireCanvas;
    [SerializeField] GameObject SpawnPhase1_1;
    [SerializeField] GameObject SpawnPhase1_2;
    
    private int _stateCombat = -1;
    private bool _phase1Instanciate;
    private bool _phase2Instanciate;
    private bool _phase3Instanciate;

    private GameObject PetitGroupeEnemi1;
    private GameObject PetitGroupeEnemi2;
    private GameObject PetitGroupeEnemi3;

    private GameObject GrandGroupeEnemi1;
    private GameObject GrandGroupeEnemi2;

    private void Start()
    {
        VictoireCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // print(_stateCombat);
        print(GameManager.Instance.NumberMonsterKill);
        if (_stateCombat == 1)
        {
            
            if (!_phase1Instanciate)
            {
                print("Debut Phase 1");
                PetitGroupeEnemi1 = Instantiate(PetitGroupeEnemi);
                // oldChild.transform.parent = Weapon.transform;
                // PetitGroupeEnemi1.transform.parent = SpawnPhase1_1.transform;
                PetitGroupeEnemi1.GetComponent<Transform>().position = new Vector3(45.45f, 13.10235f, 61.14f);
                // PetitGroupeEnemi1.transform.name = ChangeString(PetitGroupeEnemi1.transform.name);
                // PetitGroupeEnemi1.transform.localPosition = new Vector3(0f, 0f, 0f);
                // PetitGroupeEnemi1.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
                // PetitGroupeEnemi1.transform.localScale = Vector3.one;
                
                PetitGroupeEnemi2 = Instantiate(PetitGroupeEnemi);
                // PetitGroupeEnemi2.transform.parent = SpawnPhase1_2.transform;
                PetitGroupeEnemi2.GetComponent<Transform>().position = new Vector3(23.79629f, 13.10235f, 66.07349f);
                // PetitGroupeEnemi2.transform.name = ChangeString(PetitGroupeEnemi2.transform.name) + "1";
                // PetitGroupeEnemi2.transform.localPosition = new Vector3(0f, 0f, 0f);
                // PetitGroupeEnemi2.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
                // PetitGroupeEnemi2.transform.localScale = Vector3.one;
                
                _phase1Instanciate = true;
            }
            
            if (GameManager.Instance.NumberMonsterKill == 6)
            {
                Destroy(PetitGroupeEnemi1);
                Destroy(PetitGroupeEnemi2);
                _stateCombat = 2;
            }
            
        }
        else if (_stateCombat == 2)
        {
            
            if (!_phase2Instanciate)
            {
                print("Debut Phase 2");
                GrandGroupeEnemi1 = Instantiate(GrandGroupeEnemi);
                GrandGroupeEnemi1.GetComponent<Transform>().position = new Vector3(37.87662f, 13.10235f, 66.3833f);
                
                _phase2Instanciate = true;
            }
            
            if (GameManager.Instance.NumberMonsterKill == 12)
            {
                Destroy(GrandGroupeEnemi1);
                _stateCombat = 3;
            }
        }
        else if (_stateCombat == 3)
        {
            if (!_phase3Instanciate)
            {
                print("Debut Phase 3");
                GrandGroupeEnemi2 = Instantiate(GrandGroupeEnemi);
                GrandGroupeEnemi2.GetComponent<Transform>().position = new Vector3(25.60651f, 13.10235f, 47.27994f);
            
                PetitGroupeEnemi3 = Instantiate(PetitGroupeEnemi);
                PetitGroupeEnemi3.GetComponent<Transform>().position = new Vector3(41.79845f, 13.10235f, 48.45648f);

                _phase3Instanciate = true;
            }
            
            if (GameManager.Instance.NumberMonsterKill == 21)
            {
                Destroy(GrandGroupeEnemi2);
                Destroy(PetitGroupeEnemi3);
                _stateCombat = 4;
            }
        }
        else if (_stateCombat == 4)
        {
            print("Fin du jeu !!!!");
            VictoireCanvas.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _stateCombat = 1;
        }
    }
    
    private string ChangeString(string str) // On enlève les 7 dernier char pour éviter que il y est des "(Clone)" dans le nom
    {
        int newLength = str.Length - 7; // 7 est la longeur de: (Clone).
        string newString = str.Substring(0, newLength);
        return newString;
    }
}
