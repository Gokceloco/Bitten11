using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public float walkSpeed;
    public float runSpeed;

    public float speed;
    public float jumpPower;

    private Rigidbody _rb;

    private bool _isGrounded;

    public LayerMask lookLayers;

    public bool isDead;

    public HealthBar healthBar;

    public int startHealth;
    private int _currentHealth;

    public MoveControls moveControls;

    private Animator _animator;
    private string _curAnimationKey;

    private Vector3 _direction;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
        _currentHealth = startHealth;
        healthBar.SetFillBar(1);

        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _animator = GetComponentInChildren<Animator>();
    }

    void SwitchAnimationState(string key, bool forceAnimation = false)
    {
        if (_curAnimationKey != key || forceAnimation)
        {
            _animator.CrossFade(key, .05f);
            _curAnimationKey = key;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameDirector.gameState != GameState.GamePlay)
        {
            _rb.linearVelocity = Vector3.zero;
            return;
        }

        if (moveControls == MoveControls.WASD)
        {
            MovePlayer();

            _isGrounded = CheckIfGrounded();

            Jump();

            LookAtMouse();
        }
        else
        {

        }


        if (transform.position.y < -10)
        {
            gameDirector.LevelFailed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            other.gameObject.SetActive(false);
            gameDirector.LevelCompleted();
        }
    }

    private void LookAtMouse()
    {
        if (Physics.Raycast(Camera.main.transform.position,
            Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).direction,
            out var hit,
            50,
            lookLayers))
        {
            var lookPos = hit.point;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);


            var signedAngle = Vector3.SignedAngle(transform.forward, _direction, Vector3.up);

            _animator.SetFloat("RunBlend", signedAngle);
        }

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

        _direction = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            _direction += Vector3.forward;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            _direction += Vector3.left;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            _direction += Vector3.back;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            _direction += Vector3.right;
        }

        var yVelocity = _rb.linearVelocity;

        yVelocity.x = 0;
        yVelocity.z = 0;

        _rb.linearVelocity = _direction.normalized * speed + yVelocity;

        if (_rb.linearVelocity.magnitude > 0)
        {
            SwitchAnimationState("Run");
        }
        else
        {
            SwitchAnimationState("Idle");
        }
    }

    public void GetHit()
    {
        _currentHealth--;
        healthBar.SetFillBar((float)_currentHealth / startHealth);
        if (_currentHealth <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
            gameDirector.LevelFailed();
        }
    }
}

public enum MoveControls
{
    WASD,
    Touch,
}