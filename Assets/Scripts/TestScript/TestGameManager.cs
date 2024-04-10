using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public static TestGameManager Instance;

    public GameObject Player;
    private void Start()
    {
        Cursor.visible = false; // Affiche le curseur de la souris
        Cursor.lockState = CursorLockMode.Locked;
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