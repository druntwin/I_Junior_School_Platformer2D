using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _characterRoot;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private Animator _animator;

    private int _healthHash = Animator.StringToHash("Health");
    private int _hitHash = Animator.StringToHash("Hit");

    private float _previousHealth;

    public void UpdateHealthAnimator(float health)
    {
        _animator.SetFloat(_healthHash, health);

        if (health < _previousHealth)
            _animator.SetTrigger(_hitHash);

        if (health <= 0)
            _capsuleCollider.enabled = false;

        _previousHealth = health;
    }

    private void Kill()
    {
        Destroy(_characterRoot);
    }
}
