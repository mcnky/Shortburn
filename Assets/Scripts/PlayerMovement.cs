using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 4f;

    public float mouseSensitivity = 200f;
    public Transform playerCamera;
    private float xRotation = 0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    public float crouchSpeed = 2f; 
    private bool isCrouching = true;
    private Vector3 originalScale;
    private Vector3 crouchScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        originalScale = transform.localScale;
        crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float adjustedSpeed = isCrouching ? crouchSpeed : moveSpeed;
        rb.MovePosition(rb.position + move * adjustedSpeed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                Crouch();
            }
        }
        else if (isCrouching)
        {
            isCrouching = false;
            StandUp();
        }
    }

    void Crouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - (originalScale.y - crouchScale.y), transform.position.z);
    }

    void StandUp()
    {
        transform.localScale = originalScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + (originalScale.y - crouchScale.y) / 2, transform.position.z);
    }
}