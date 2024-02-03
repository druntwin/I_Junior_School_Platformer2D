using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _additionalHealth;
    [SerializeField] private LayerMask _collisionLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _collisionLayerMask)
        {
            collision.transform.TryGetComponent(out Health health);
            if (health != null)
                health.AddHealth(_additionalHealth);
        }
    }
}
