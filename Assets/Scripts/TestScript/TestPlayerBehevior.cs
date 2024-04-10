using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerBehevior : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    
    [SerializeField] Transform mainCamera; // Référence à la Transforme de la caméra principale
    
    private bool _isGrounded = true;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Direction de la camera
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rightRelative = moveSpeed * cameraRight;
            _rb.AddForce(rightRelative);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rightRelative = -moveSpeed * cameraRight;
            _rb.AddForce(rightRelative);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forwardRelative = moveSpeed * cameraForward;
            _rb.AddForce(forwardRelative);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 forwardRelative = -moveSpeed * cameraForward;
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
