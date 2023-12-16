using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    public CameraController cameraController;

    public float moveSpeed;
    public float jumpForce;
    public Vector2 moveVector;
    public bool jumping;

    private Rigidbody rb;

    private bool canJump;
    private bool backwards;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDirection = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 horizontalCameraOffset = transform.position - Camera.main.transform.position;
        horizontalCameraOffset.y = 0.0f;

        if (!backwards)
        {
            moveDirection = horizontalCameraOffset.normalized;
        }

        // (3.6f + (3.5f - 1.0f)) = Collider center + (Collider height / 2 - Collider radius)
        // Penguin model is scaled down by 1/7
        RaycastHit hit; 
        if (Physics.CapsuleCast(transform.position + transform.up * (3.8f + (3.5f - 1.0f)) / 7.0f, transform.position + transform.up * (3.8f - (3.5f - 1.0f)) / 7.0f, 0.9f / 7.0f, Vector3.down, out hit, 0.2f))
        {
            canJump = true;
            if (Vector3.Angle(Vector3.up, hit.normal) < 0.01f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), 15.0f);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-hit.normal, moveDirection), 2.0f);
            }
        }
        else
        {
            canJump = false;

        }

        cameraController.Offset = Vector3.RotateTowards(
            cameraController.Offset,
            Quaternion.FromToRotation(Vector3.up, hit.normal) * cameraController.InitialOffset,
            0.02f,
            0.0f);

        rb.AddForce(Quaternion.FromToRotation(Vector3.forward, moveDirection) * new Vector3(moveVector.x, 0.0f, moveVector.y) * moveSpeed);
        if (canJump && jumping)
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
        }

        jumping = false;
    }

    void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();
        if (moveVector.y < 0.0f)
        {
            backwards = true;
        }
        else
        {
            backwards = false;
        }
    }

    void OnJump(InputValue value)
    {
        jumping = value.Get<float>() > 0.0f;
    }
}
