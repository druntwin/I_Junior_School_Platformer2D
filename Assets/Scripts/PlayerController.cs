using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const string JumpTriggerName = "Jump";
    private const string FallTriggerName = "Fall";
    private const string CrouchBoolName = "IsCrouch";
    private const string GroundedBoolName = "IsGrounded";
    private const string SpeedFloatName = "Speed";

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _boxCollaider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GroundDetection _groundDetection;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _speed = 5f;

    private Vector2 _direction;
    private bool _isJumping;
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
        if( _isJumping == false )
            Jump();

        Move();
    }

    private void Update()
    {
        Crouch();
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _animator.SetTrigger(JumpTriggerName);
            _isJumping = true;
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
        _animator.SetBool(GroundedBoolName, _groundDetection.IsGrounded);

        _isJumping = _isJumping == true && _groundDetection.IsGrounded == false;
        _direction = Vector2.zero;

        if (_rigidbody.velocity.y < 0)
            _animator.SetTrigger(FallTriggerName);

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
