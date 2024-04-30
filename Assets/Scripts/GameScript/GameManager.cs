using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;
    public GameObject PlayerHand;
    
    private void Start()
    {
        Cursor.visible = false; // Affiche le curseur de la souris
        Cursor.lockState = CursorLockMode.Locked; // Locke la souris au milieu de l'écran
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
