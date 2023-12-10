using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<PlayerController>(out PlayerController playerController);

        if (playerController != null)
            Destroy(playerController.gameObject);
    }
}
