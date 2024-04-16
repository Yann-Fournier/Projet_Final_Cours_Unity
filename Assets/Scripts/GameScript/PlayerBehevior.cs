using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehevior : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float jumpForce;
    
    [SerializeField] Transform mainCamera; // Référence à la Transforme de la caméra principale
    
    private bool _isGrounded = true;
    private Rigidbody _rb;
    private Transform _t;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        
        // Direction de la camera
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = moveSpeed + sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.CapsLock))
        {
            speed = moveSpeed - crouchSpeed;
            _t.localScale = new Vector3(1f, 0.6f, 1f);
        }
        else
        {
            speed = moveSpeed;
            _t.localScale = new Vector3(1f, 1f, 1f);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rightRelative = speed * cameraRight;
            _rb.AddForce(rightRelative);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rightRelative = -speed * cameraRight;
            _rb.AddForce(rightRelative);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forwardRelative = speed * cameraForward;
            _rb.AddForce(forwardRelative);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 forwardRelative = -speed * cameraForward;
            _rb.AddForce(forwardRelative);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(new Vector3(0, jumpForce, 0));
            _isGrounded = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }
}