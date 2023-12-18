using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out Player player);

        if (player != null)
            Destroy(player.gameObject);
    }
}
