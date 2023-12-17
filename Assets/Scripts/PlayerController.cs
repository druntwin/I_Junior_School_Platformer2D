using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const string JumpBoolName = "Jump";
    private const string FallBoolName = "Fall";
    private const string CrouchBoolName = "Crouch";
    private const string GroundedBoolName = "Grounded";
    private const string SpeedFloatName = "Speed";

    [SerializeField] private BoxCollider2D _boxCollaider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GroundDetection _groundDetection;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private bool _isJumping;
    private bool _isFalling;
    private bool _isCrouch;

    private Vector2 _colliderNormalSize = new Vector2(1f, 1.38f);
    private Vector2 _colliderNormalOffset = new Vector2(-0.03f, -0.29f);
    private Vector2 _colliderCrouchSize = new Vector2(1.12f, 1f);
    private Vector2 _colliderCrouchOffset = new Vector2(-0.09f, -0.49f);

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

    private void Jump()
    {
        if (_isJumping == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                _isJumping = true;
                _animator.SetBool(JumpBoolName, _isJumping);
            }
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _isCrouch = true;
            _boxCollaider2D.size = _colliderCrouchSize;
            _boxCollaider2D.offset = _colliderCrouchOffset;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _isCrouch = false;
            _boxCollaider2D.size = _colliderNormalSize;
            _boxCollaider2D.offset = _colliderNormalOffset;
        }
        
        _animator.SetBool(CrouchBoolName, _isCrouch);
    }

    private void Move()
    {
        _direction = Vector2.zero;
        _isJumping = _isJumping == true && _groundDetection.IsGrounded == false;

        if (_rigidbody.velocity.y < 0)
        {
            _isFalling = _groundDetection.IsGrounded == false;
        }

        _animator.SetBool(JumpBoolName, _isJumping);
        _animator.SetBool(FallBoolName, _isFalling);
        _animator.SetBool(GroundedBoolName, _groundDetection.IsGrounded);

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

            _animator.SetFloat(SpeedFloatName, Mathf.Abs(_direction.x));
        }
    }
}
