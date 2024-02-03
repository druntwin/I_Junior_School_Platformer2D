using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            if(player != null)
            {
                collision.transform.TryGetComponent(out Health health);

                if (health != null)
                    health.Reduse(_damage);
            }
        }
    }
}