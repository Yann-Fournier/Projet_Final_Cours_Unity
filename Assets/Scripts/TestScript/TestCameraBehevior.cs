using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestCameraBehevior : MonoBehaviour
{
    public Transform player;
    public Transform pointCamera;
    public float sensitivity;
    
    private float mouseX;
    private Transform _t;
    private Vector3 _midScreen;
    private bool _delock;

    private void Start()
    {
        _t = GetComponent<Transform>();
        _midScreen = Input.mousePosition;
        _delock = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;// Verrouille le curseur au centre de la fenêtre du jeu
        }
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.None; // Libère le curseur pour qu'il puisse se déplacer librement
            if (Input.GetAxis("Mouse X") != 0 || _delock)
            {
                mouseX += Input.GetAxis("Mouse X") * sensitivity;
                _delock = true;
            }
            else
            {
                _delock = false;
                return;
            }
            

            // Appliquer la rotation de la caméra autour du joueur
            Vector3 offset = transform.position - player.position; // Calculer l'offset de la caméra par rapport au joueur
            Quaternion rotation = Quaternion.Euler(0, mouseX, 0); // Créer une rotation autour de l'axe Y
            offset = rotation * offset; // Appliquer la rotation à l'offset
            transform.position = player.position + offset; // Définir la nouvelle position de la caméra
            // Vector3 pos = new Vector3(player.position.x + 16.565f, player.position.y, player.position.z);
            transform.LookAt(pointCamera.position); // Orienter la caméra vers le joueur
            _t.rotation.Set(rotation.x, 46.764f,rotation.z,rotation.w);
        }
    }

    private bool MouseNotInMiddle(Vector3 middle)
    {
        if ((middle.x < middle.x + 50) || (middle.x > middle.x + 50))
        {
            return true;
        }
        return false;
    }
}
