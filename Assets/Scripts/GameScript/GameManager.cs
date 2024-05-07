using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;
    
    public GameObject PlayerHand;
    public bool IsAttaking;

    public bool PlayerIsDead;
    public Vector3 RespawnPoint;

    public int NumberMonsterKill;
    private void Start()
    {
        Cursor.visible = false; // Affiche le curseur de la souris
        Cursor.lockState = CursorLockMode.Locked; // Locke la souris au milieu de l'écran
        RespawnPoint = Player.transform.position;
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
