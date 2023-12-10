using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody = new Rigidbody2D();
    [SerializeField] private BoxCollider2D _boxCollaider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GroundDetection _groundDetection;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _speed = 5f;

    private Vector2 _direction;
    private bool _isJumping;
    private bool _isCrouch;


    private void FixedUpdate()
    {
        Move();

        if( _isJumping == false )
            Jump();        
    }

    private void Update()
    {
        Crouch();
    }

    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
            _isJumping = true;
        }
    }

    public void Crouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _isCrouch = true;
            _boxCollaider2D.size = new Vector2(1.12f, 1f);
            _boxCollaider2D.offset = new Vector2(-0.09f, -0.49f);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _isCrouch = false;
            _boxCollaider2D.size = new Vector2(1f, 1.38f);
            _boxCollaider2D.offset = new Vector2(-0.03f, -0.29f);
        }
        
        _animator.SetBool("IsCrouch", _isCrouch);
    }

    private void Move()
    {
        _animator.SetBool("IsGrounded", _groundDetection.IsGrounded);

        if (_isJumping == false && _groundDetection.IsGrounded == false)
            _animator.SetTrigger("Fall");

        _isJumping = _isJumping == true && _groundDetection.IsGrounded == false;
        _direction = Vector2.zero;

        if(_isCrouch == false)
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

            _animator.SetFloat("Speed", Mathf.Abs(_direction.x));
        }
    }
}
