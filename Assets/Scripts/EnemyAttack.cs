using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private int _damage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PlayerTag)
        {
            collision.transform.TryGetComponent(out Health health);

            if (health != null)
                health.ReduseHealth(_damage);
        }
    }
}