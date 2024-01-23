using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _additionalHealth;
    [SerializeField] private string _collisionTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _collisionTag)
        {
            collision.transform.TryGetComponent(out Health health);
            if (health != null)
                health.AddHealth(_additionalHealth);
        }
    }
}
