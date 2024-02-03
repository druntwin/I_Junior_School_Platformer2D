using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Health _health;

    public void TakeDamage(int damage)
    {
        _health.ReduseHealth(damage);
    }
}
