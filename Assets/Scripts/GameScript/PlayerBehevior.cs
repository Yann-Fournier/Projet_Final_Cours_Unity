using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehevior : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float jumpForce;
    
    [SerializeField] Transform mainCamera; // Référence à la Transforme de la caméra principale
    [SerializeField] Animator CharacterAnimator;
    
    private bool _isGrounded = true;
    private bool _isCrouch;
    private Rigidbody _rb;
    private Transform _t;
    private float _rotationSpeed = 8f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed; // vitesse de déplacement
        
        // Orientation de la camera
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;
        cameraForward.y = 0; // Pour éviter que le joueur ce retourne
        cameraRight.y = 0;
        
        if (Input.GetKey(KeyCode.LeftShift) && !_isCrouch) // Sprinter
        {
            speed = moveSpeed + sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.CapsLock)) // S'accroupir
        {
            CharacterAnimator.SetBool("IsCrouching", true);
            speed = moveSpeed - crouchSpeed;
            _t.localScale = new Vector3(1f, 0.6f, 1f);
            _isCrouch = true;
        }
        else // Vitesse normal
        {
            speed = moveSpeed;
            _t.localScale = new Vector3(1f, 1f, 1f);
        }
        
        if (Input.GetKeyUp(KeyCode.CapsLock)) // Se relever
        {
            CharacterAnimator.SetBool("IsCrouching", false);
            _isCrouch = false;
        }
        
        if (Input.GetKey(KeyCode.D)) // droite
        {
            // Rotation
            Quaternion targetRotation = Quaternion.LookRotation(cameraRight);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            // Position
            Vector3 rightRelative = speed * cameraRight;
            _rb.AddForce(rightRelative);
        }
        if (Input.GetKey(KeyCode.A)) // gauche
        {
            // Rotation
            Quaternion targetRotation = Quaternion.LookRotation(-cameraRight);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            //Position
            Vector3 rightRelative = -speed * cameraRight;
            _rb.AddForce(rightRelative);
        }
        if (Input.GetKey(KeyCode.W)) // avant 
        {
            // Rotation
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            // Position
            Vector3 forwardRelative = speed * cameraForward;
            _rb.AddForce(forwardRelative);
        }
        if (Input.GetKey(KeyCode.S)) // arrière
        {
            // Rotation
            Quaternion targetRotation = Quaternion.LookRotation(-cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            // Position
            Vector3 forwardRelative = -speed * cameraForward;
            _rb.AddForce(forwardRelative);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) // Saut
        {
            _rb.AddForce(new Vector3(0, jumpForce, 0));
            _isGrounded = false;
        }
        
        // Récupération de la vitesse pour faire les animations 
        CharacterAnimator.SetFloat("Speed", _rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }
}