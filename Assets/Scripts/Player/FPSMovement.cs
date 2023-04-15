using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    float inputMoveSpeed;

    public float groundDrag = 5;

    public float jumpForce = 5;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 2;
    bool readyToJump = true;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    bool grounded = true;

    Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;
        orientation = transform;

        readyToJump = true;
        inputMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && !grounded)
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6 && grounded)
        {
            grounded = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        if(CheatManager.is2xSpeed)
        {
            moveSpeed = inputMoveSpeed * 2;
        }
        else
        {
            moveSpeed = inputMoveSpeed;
        }
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void KnockBack()
    {
        rb.AddForce(gameObject.transform.forward * -50f, ForceMode.Impulse);
        rb.AddForce(gameObject.transform.up * 10f, ForceMode.Impulse);
    }
}