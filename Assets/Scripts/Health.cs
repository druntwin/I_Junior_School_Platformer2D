using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    [SerializeField] private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }

    public void AddHealth(int health)
    {
        _health += health;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }
}
