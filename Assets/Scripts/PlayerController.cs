using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movment")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpForce;

    [Header("Key Binds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Grounded Handling")]
    [SerializeField] private LayerMask[] groundLayerMasks;


    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private Vector3 direction = Vector3.zero;
    private float currentSprint;
    private bool isGrounded = false;
    private bool isTired = false;
    private bool isRunning = false;
    private bool isMoving = false;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetDirection();
    }

    private void GetDirection()
    {
        float xDirection = Input.GetAxis("Vertical");
        float zDirection = Input.GetAxis("Horizontal");

        if (xDirection != 0 || zDirection != 0)
            isMoving = true; 
        else
            isMoving = false;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        direction = (forward * xDirection) + (right * zDirection);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetKey(jumpKey) && (isGrounded))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Vector3 newVelocity;
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            newVelocity = new Vector3(direction.x * sprintSpeed, rb.velocity.y, direction.z * sprintSpeed);
            isRunning = true;
        }
        else
        {
            newVelocity = new Vector3(direction.x * walkSpeed, rb.velocity.y, direction.z * walkSpeed);
            isRunning = false;
        }

        rb.velocity = newVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        LayerMask collisionLayer = 1 << collision.gameObject.layer;
        if (Utilities.ArrayContains(groundLayerMasks, collisionLayer))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        LayerMask collisionLayer = 1 << collision.gameObject.layer;
        if (Utilities.ArrayContains(groundLayerMasks, collisionLayer))
            isGrounded = false;
    }

    public float CurrentSprint { get => currentSprint; }
    public bool IsTired { get => isTired; }

}
