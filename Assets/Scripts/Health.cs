using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{    
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;

    public UnityEvent<float> HealthChanged;

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged.Invoke(_health);        
    }

    public void Reduse(int reducer)
    {
        _health -= reducer;
        HealthChanged.Invoke(_health);
    }

    public void Add(int health)
    {
        _health += health;

        if (_health > _maxHealth)
            _health = _maxHealth;

        HealthChanged.Invoke(_health);
    }
}
