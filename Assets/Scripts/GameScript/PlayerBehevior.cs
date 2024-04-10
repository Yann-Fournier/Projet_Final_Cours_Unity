using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehevior : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float RotationSpeed;
    
    // public Camera mainCamera; // Référence à la caméra principale
    // public float movementSpeed = 5.0f; // Vitesse de déplacement du joueur
    
    private bool _isGrounded = true;

    // Update is called once per frame
    void Update()
    {
        // Obtenir la position de la caméra
        // Vector3 cameraPosition = mainCamera.transform.position;

        // Déplacer le joueur en fonction de la position de la caméra
        // En supposant que le joueur se déplace sur le plan horizontal (XZ)
        
        // Vector3 movementDirection = new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z) - transform.position;
        // movementDirection.Normalize(); // Normaliser la direction pour maintenir une vitesse constante
        // transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World); // Déplacer le joueur

        // Facultatif : limiter le déplacement pour éviter les sorties de la scène ou les collisions
        // Par exemple, vous pouvez utiliser des colliders pour définir les limites de déplacement du joueur
        
        float speed = Speed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(-speed / 3, 0, 0));
            GetComponent<Transform>().Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(speed / 3, 0, 0));
            GetComponent<Transform>().Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(GetComponent<Transform>().forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(-GetComponent<Transform>().forward * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpForce, 0));
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }
}
