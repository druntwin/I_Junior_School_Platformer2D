using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject _characterRoot;
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;

    private int _healthHash =  Animator.StringToHash("Health");
    private int _hitHash =  Animator.StringToHash("Hit");

    private void Start()
    {
        _health = _maxHealth;
        _animator.SetFloat(_healthHash, _health);
    }

    public void ReduseHealth(int reducer)
    {
        _health -= reducer;
        _animator.SetFloat(_healthHash, _health);
        _animator.SetTrigger(_hitHash);

        if (_health <= 0)
            _capsuleCollider.enabled = false;
    }

    public void AddHealth(int health)
    {
        _health += health;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void Kill()
    {
        Destroy(_characterRoot);
    }
}
