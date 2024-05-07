using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehevior : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float climbSpeed;
    
    [SerializeField] Transform mainCamera; // Référence à la Transforme de la caméra principale
    [SerializeField] Animator characterAnimator;
    
    [SerializeField] GameObject deadCanvas;
    
    private Rigidbody _rb;
    private Transform _t;
    private CapsuleCollider _c;
    private GameObject _playerHand;
    
    private float _rotationSpeed = 8f;
    private bool _isJumping;
    private bool _isCrouch;
    private bool _isOnLadder;
    private bool _isOnSpine;
    private bool _stopOnSpine;
    private bool _uppercut;
    private bool _hasWeapon;
    private bool _canRespawn;
    private bool _coRoutinehasStart;
    
    private KeyCode[] _keysINeed = { KeyCode.W , KeyCode.S, KeyCode.A, KeyCode.D };

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
        _c = GetComponent<CapsuleCollider>();
        deadCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerIsDead)
        {
            if (!_coRoutinehasStart)
            {
                _coRoutinehasStart = true;
                StartCoroutine(SetTemporaryValue_IsDead(4));
            }
            if (_canRespawn)
            {
                characterAnimator.SetFloat("Speed", 0f);
                GameManager.Instance.PlayerIsDead = false;
                deadCanvas.SetActive(false);
                _t.SetPositionAndRotation(GameManager.Instance.RespawnPoint, new Quaternion(0, 0, 0, 0));
                _canRespawn = false;
                _coRoutinehasStart = false;
            }
        } 
        else
        {
            if (_isOnLadder)
            {
                VerticalMoves();
            }
            else
            {
                HorizontalMoves();
            }
        
            // Set des variables d'Animations
            characterAnimator.SetFloat("Speed", _rb.velocity.magnitude);
            characterAnimator.SetBool("IsJumping", _isJumping);
            characterAnimator.SetBool("IsCrouching", _isCrouch);
            characterAnimator.SetBool("OnLadder", _isOnLadder);
            characterAnimator.SetBool("IsAttacking", GameManager.Instance.IsAttaking);
            characterAnimator.SetBool("HasWeapon", _hasWeapon);
            characterAnimator.SetBool("Uppercut", _uppercut);
        }
        characterAnimator.SetBool("IsDead", GameManager.Instance.PlayerIsDead);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemiesWeapon")
        {
            GameManager.Instance.PlayerIsDead= true;
        }
        if (collision.gameObject.tag == "Spine")
        {
            _isOnSpine = true;
        }
        else if (collision.gameObject.tag == "Ladder")
        {
            _isOnLadder = true;
            _rb.useGravity = false;
            _playerHand = GameManager.Instance.PlayerHand;
            if (_playerHand.transform.childCount > 0)
            {
                _playerHand.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "LadderFloor" && _isOnLadder)
        {
            _isOnLadder = false;
            _rb.useGravity = true;
            _playerHand = GameManager.Instance.PlayerHand;
            if (_playerHand.transform.childCount > 0)
            {
                _playerHand.SetActive(true);
            }
        }
        _isJumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "EnemiesWeapon")
        {
            GameManager.Instance.PlayerIsDead = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "EnemiesWeapon")
        {
            GameManager.Instance.PlayerIsDead= true;
        }
        if (collision.gameObject.tag == "Spine")
        {
            _rb.drag = 2f;
            _rb.mass = 1f;
            _isOnSpine = false;
        } 
        else if (collision.gameObject.tag == "Ladder")
        {
            _isOnLadder = false;
            _rb.useGravity = true;
            _playerHand = GameManager.Instance.PlayerHand;
            if (_playerHand.transform.childCount > 0)
            {
                _playerHand.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemiesWeapon")
        {
            GameManager.Instance.PlayerIsDead = true;
        }
    }
    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.tag == "EnemiesWeapon")
    //     {
    //         GameManager.Instance.PlayerIsDead = true;
    //     }
    // }
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "EnemiesWeapon")
    //     {
    //         GameManager.Instance.PlayerIsDead = true;
    //     }
    // }

    private void HorizontalMoves()
    {
        float speed; // vitesse de déplacement
        
        // Orientation de la camera
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;
        cameraForward.y = 0; // Pour éviter que le joueur ce retourne
        cameraRight.y = 0;
        
        if (Input.GetKey(KeyCode.LeftShift) && !_isCrouch) // Sprinter
        {
            speed = (moveSpeed + sprintSpeed) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.CapsLock)) // S'accroupir
        {
            speed = (moveSpeed - crouchSpeed) * Time.deltaTime;
            _c.height = 1.5f;
            _c.center = new Vector3(0, -0.21f, 0);
            _isCrouch = true;
        }
        else // Vitesse normal
        {
            speed = moveSpeed * Time.deltaTime;
            _t.localScale = new Vector3(1f, 1f, 1f);
        }
        
        if (Input.GetKeyUp(KeyCode.CapsLock)) // Se relever
        {
            
            _c.height = 1.9f;
            _c.center = new Vector3(0, 0, 0);
            _isCrouch = false;
        }
        
        if (_isOnSpine && _rb.velocity.magnitude < 10)
        {
            if (AllKeysUp())
            {
                _rb.constraints = RigidbodyConstraints.FreezePosition;
                _rb.freezeRotation = true;
            }
            else
            {
                _rb.constraints = RigidbodyConstraints.None;
                _rb.freezeRotation = true;
                speed += (500 * Time.deltaTime);
            }
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
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping && !_isCrouch) // Saut
        {
            _rb.AddForce(new Vector3(0, jumpForce, 0));
            _isJumping = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) // attaque
        {
            _rb.velocity = new Vector3(0, 0, 0);
            GameManager.Instance.IsAttaking = true;
            if (GameManager.Instance.PlayerHand.transform.childCount == 0 && GameManager.Instance.IsAttaking)
            {
                _hasWeapon = false;
                if (_uppercut)
                {
                    _uppercut = false;
                }
                else if (!_uppercut)
                {
                    _uppercut = true;
                }
                StartCoroutine(SetTemporaryValue_IsAttacking(1));
            } 
            else if (GameManager.Instance.PlayerHand.transform.childCount > 0 && GameManager.Instance.IsAttaking)
            {
                _hasWeapon = true;
                StartCoroutine(SetTemporaryValue_IsAttacking(1.5f));
            }
            
        }
    }

    private void VerticalMoves()
    {
        float verticalInput = Input.GetAxis("Vertical"); // mouvement vertical
        transform.Translate(Vector3.up * verticalInput * climbSpeed * Time.deltaTime);
    }

    private bool AllKeysUp()
    {
        bool noKeyPressed = true;
        foreach (KeyCode key in _keysINeed)
        {
            if (Input.GetKey(key))
            {
                noKeyPressed = false;
                break;
            }
        }
        return noKeyPressed;
    }
    
    IEnumerator SetTemporaryValue_IsAttacking(float time)
    {
        GameManager.Instance.IsAttaking = true;
        yield return new WaitForSeconds(time);
        GameManager.Instance.IsAttaking = false;
    }
    
    IEnumerator SetTemporaryValue_IsDead(float time)
    {
        deadCanvas.SetActive(true);
        yield return new WaitForSeconds(time);
        _canRespawn = true;
    }
}
