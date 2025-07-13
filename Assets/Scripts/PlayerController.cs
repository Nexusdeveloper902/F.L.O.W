using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveSpeedRunning = 5f;
    
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lookSensitivity = 2f;
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Interacting")]
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float maxInteractDistance = 10f;

    private Rigidbody rb;
    private float cameraRotationX = 0f;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();
        Debug.Log(CanInteract());

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
        
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        var movement = moveSpeed;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = moveSpeedRunning;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movement = moveSpeed;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move *= movement;

        Vector3 velocity = new Vector3(move.x, rb.velocity.y, move.z);
        rb.velocity = velocity;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    bool CanInteract()
    {
        return Physics.Raycast(cameraTransform.position, cameraTransform.forward, maxInteractDistance,interactLayer);
    }
}