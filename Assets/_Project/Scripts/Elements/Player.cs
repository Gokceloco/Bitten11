using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public float speed;
    public float jumpPower;

    private Rigidbody _rb;

    private bool _isGrounded;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();

        _isGrounded = CheckIfGrounded();

        Jump();
    }

    private bool CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position + Vector3.up * .5f, Vector3.down, 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump()
    {
        if (_isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _rb.AddForce(Vector3.up * jumpPower);
        }
    }

    private void MovePlayer()
    {
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        var direction = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            direction += Vector3.forward;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            direction += Vector3.left;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            direction += Vector3.back;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            direction += Vector3.right;
        }

        var yVelocity = _rb.linearVelocity;

        yVelocity.x = 0;
        yVelocity.z = 0;

        _rb.linearVelocity = direction.normalized * speed + yVelocity;        
    }
}
