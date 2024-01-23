using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int _damage = 0;
    [SerializeField] private LayerMask _collideLayerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _collideLayerMask)
        {
            collision.transform.TryGetComponent(out Health health);
            health.TakeDamage(_damage);
        }
    }
}
