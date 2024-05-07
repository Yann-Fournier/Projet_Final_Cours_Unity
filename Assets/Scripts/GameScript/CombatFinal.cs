using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatFinal : MonoBehaviour
{
    [SerializeField] GameObject VictoireCanvas;
    [SerializeField] GameObject PetitGroupeEnemi1;
    [SerializeField] GameObject PetitGroupeEnemi2;
    [SerializeField] GameObject GrandGroupeEnemi1;
    [SerializeField] GameObject PetitGroupeEnemi3;
    [SerializeField] GameObject GrandGroupeEnemi2;
    
    private int _stateCombat = -1;
    private bool _phase1Instanciate;
    private bool _phase2Instanciate;
    private bool _phase3Instanciate;
    
    private void Start()
    {
        VictoireCanvas.SetActive(false);
        PetitGroupeEnemi1.SetActive(false);
        PetitGroupeEnemi2.SetActive(false);
        PetitGroupeEnemi3.SetActive(false);
        GrandGroupeEnemi1.SetActive(false);
        GrandGroupeEnemi2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Reset le combat si il est mort pendant
        if (GameManager.Instance.PlayerIsDead && GameManager.Instance.RespawnPoint == new Vector3(35.42328f, 13.018f, 38.32f))
        {
            StartCoroutine(RestartCombat(2));
            
            _stateCombat = 1;
            _phase1Instanciate = false;
            _phase2Instanciate = false;
            _phase3Instanciate = false;
        }
        if (_stateCombat == 1)
        {
            
            if (!_phase1Instanciate)
            {
                print("Debut Phase 1");
                PetitGroupeEnemi1.SetActive(true);
                PetitGroupeEnemi2.SetActive(true);
                _phase1Instanciate = true;
            }
            
            if (GameManager.Instance.NumberMonsterKill == 6)
            {
                PetitGroupeEnemi1.SetActive(false);
                PetitGroupeEnemi2.SetActive(false);
                _stateCombat = 2;
            }
            
        }
        else if (_stateCombat == 2)
        {
            
            if (!_phase2Instanciate)
            {
                print("Debut Phase 2");
                GrandGroupeEnemi1.SetActive(true);
                _phase2Instanciate = true;
            }
            
            if (GameManager.Instance.NumberMonsterKill == 12)
            {
                GrandGroupeEnemi1.SetActive(false);
                _stateCombat = 3;
            }
        }
        else if (_stateCombat == 3)
        {
            if (!_phase3Instanciate)
            {
                print("Debut Phase 3");
                GrandGroupeEnemi2.SetActive(true);
                PetitGroupeEnemi3.SetActive(true);
                _phase3Instanciate = true;
            }
            
            if (GameManager.Instance.NumberMonsterKill == 21)
            {
                GrandGroupeEnemi2.SetActive(false);
                PetitGroupeEnemi3.SetActive(false);
                _stateCombat = 4;
            }
        }
        else if (_stateCombat == 4)
        {
            VictoireCanvas.SetActive(true);
            StartCoroutine(ReloadCurrentScene(5));
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
    
    IEnumerator ReloadCurrentScene(float time)
    {
        GameManager.Instance.Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(time);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Obtenir l'index de la scène actuelle
        SceneManager.LoadScene(currentSceneIndex); // Recharger la scène par index
    }
    
    private void ReinstanciatePetitGroupeEnemies(GameObject groupe)
    {
        // Groupe -> Enemie ->Emplacement départ -> Goblin
        groupe.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
    }
    
    private void ReinstanciateGrandGroupeEnemies(GameObject groupe)
    {
        // Groupe -> Enemie ->Emplacement départ -> Goblin
        groupe.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        groupe.transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
    }
    
    IEnumerator RestartCombat(float time)
    {
        GameManager.Instance.RestartCombat = true;
        ReinstanciatePetitGroupeEnemies(PetitGroupeEnemi1);
        ReinstanciatePetitGroupeEnemies(PetitGroupeEnemi2);
        ReinstanciateGrandGroupeEnemies(GrandGroupeEnemi1);
        ReinstanciateGrandGroupeEnemies(GrandGroupeEnemi2);
        ReinstanciatePetitGroupeEnemies(PetitGroupeEnemi3);
        yield return new WaitForSeconds(time);
        PetitGroupeEnemi1.SetActive(true);
        PetitGroupeEnemi2.SetActive(true);
        GrandGroupeEnemi1.SetActive(true);
        GrandGroupeEnemi2.SetActive(true);
        PetitGroupeEnemi3.SetActive(true);
        yield return new WaitForSeconds(time);
        GrandGroupeEnemi1.SetActive(false);
        GrandGroupeEnemi2.SetActive(false);
        PetitGroupeEnemi3.SetActive(false);
        GameManager.Instance.RestartCombat = false;
    }
}
