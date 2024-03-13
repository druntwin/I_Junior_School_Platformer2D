using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _characterRoot;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GroundDetection _groundDetection;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _attackJumpForce = 0.5f;
    [SerializeField] private float _speed = 5f;

    private int _jumpHash = Animator.StringToHash("Jump");
    private int _fallHash = Animator.StringToHash("Fall");
    private int _crouchHash = Animator.StringToHash("Crouch");
    private int _groundedHash = Animator.StringToHash("Grounded");
    private int _speedHash = Animator.StringToHash("Speed");
    private int _healthHash = Animator.StringToHash("Health");
    private int _hitHash = Animator.StringToHash("Hit");

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private bool _isJumping;
    private bool _isFalling;
    private bool _isCrouch;

    private float _previousHealth;

    private Vector2 _colliderNormalSize = new Vector2(1.05f, 1.3f);
    private Vector2 _colliderNormalOffset = new Vector2(-0.055f, -0.38f);
    private Vector2 _colliderCrouchSize = new Vector2(1.05f, 1.05f);
    private Vector2 _colliderCrouchOffset = new Vector2(-0.055f, -0.5f);

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Crouch();        
        Jump();
    }

    public void UpdateHealthAnimator(float health)
    {
        _animator.SetFloat(_healthHash, health);

        if(health < _previousHealth)
            _animator.SetTrigger(_hitHash);

        if (health <= 0)
            _capsuleCollider.enabled = false;

        _previousHealth = health;
    }

    private void Kill()
    {
        _characterRoot.SetActive(false);
    }

    private void Jump()
    {
        if (_isJumping == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                _isJumping = true;
                _animator.SetBool(_jumpHash, _isJumping);
            }
        }
    }

    public void DoAttackJump()
    {
        _rigidbody.AddForce(new Vector2(0, _attackJumpForce), ForceMode2D.Impulse);
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _isCrouch = true;
            _capsuleCollider.size = _colliderCrouchSize;
            _capsuleCollider.offset = _colliderCrouchOffset;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _isCrouch = false;
            _capsuleCollider.size = _colliderNormalSize;
            _capsuleCollider.offset = _colliderNormalOffset;
        }
        
        _animator.SetBool(_crouchHash, _isCrouch);
    }

    private void Move()
    {
        _direction = Vector2.zero;
        _isJumping = _isJumping == true && _groundDetection.IsGrounded == false;

        if (_rigidbody.velocity.y < 0)
        {
            _isFalling = _groundDetection.IsGrounded == false;
        }

        _animator.SetBool(_jumpHash, _isJumping);
        _animator.SetBool(_fallHash, _isFalling);
        _animator.SetBool(_groundedHash, _groundDetection.IsGrounded);

        if (_isCrouch == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _direction = Vector2.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _direction = Vector2.right;
            }

            _direction *= _speed;
            _direction.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _direction;

            if (_direction.x > 0)
            {
                _spriteRenderer.flipX = false;
            }

            if (_direction.x < 0)
            {
                _spriteRenderer.flipX = true;
            }

            _animator.SetFloat(_speedHash, Mathf.Abs(_direction.x));
        }
    }
}
