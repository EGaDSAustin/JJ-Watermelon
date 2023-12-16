using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    public Transform ModelTransform;
    public Transform NormalTransform;

    public float MoveSpeed;
    public float JumpForce;
    
    private Rigidbody rb;

    private bool canJump;
    private bool backwards;
    private Vector3 moveDirection;
    private Vector2 moveVector;
    private bool jumping;

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
        // Feet cast || belly cast
        if (Physics.CapsuleCast(ModelTransform.position + ModelTransform.up * (3.5f + (3.5f - 1.0f)) / 7.0f, ModelTransform.position + ModelTransform.up * (3.5f - (3.5f - 1.0f)) / 7.0f, 0.8f / 7.0f, -ModelTransform.up, out RaycastHit hit, 0.2f) ||
            Physics.CapsuleCast(ModelTransform.position + ModelTransform.up * (3.5f + (3.5f - 1.0f)) / 7.0f, ModelTransform.position + ModelTransform.up * (3.5f - (3.5f - 1.0f)) / 7.0f, 0.8f / 7.0f, ModelTransform.forward, out hit, 0.2f))
        {
            canJump = true;
            if (Vector3.Angle(Vector3.up, hit.normal) < 1.0f) // On flat ground
            {
                ModelTransform.rotation = Quaternion.RotateTowards(ModelTransform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), 15.0f);
                NormalTransform.up = Vector3.up;
            }
            else // On slope
            {
                ModelTransform.rotation = Quaternion.RotateTowards(ModelTransform.rotation, Quaternion.LookRotation(-hit.normal, moveDirection), 2.0f);
                NormalTransform.up = hit.normal;
            }
        }
        else
        {
            canJump = false;
        }

        rb.AddForce(Quaternion.FromToRotation(Vector3.forward, moveDirection) * new Vector3(moveVector.x, 0.0f, moveVector.y) * MoveSpeed);
        if (canJump && jumping)
        {
            rb.AddForce(new Vector3(0.0f, JumpForce, 0.0f), ForceMode.Impulse);
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
