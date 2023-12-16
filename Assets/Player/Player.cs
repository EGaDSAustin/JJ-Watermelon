using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Vector2 moveVector;
    public bool jumping;

    private Rigidbody rb;

    private bool canJump;
    private bool backwards;
    Vector3 moveDirection;

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
        canJump = Physics.Raycast(transform.position, Vector2.down, 0.501f);

        Vector3 horizontalCameraOffset = transform.position - Camera.main.transform.position;
        horizontalCameraOffset.y = 0.0f;
        
        if (!backwards)
        {
            moveDirection = horizontalCameraOffset.normalized;
        }

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
