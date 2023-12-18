using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out Player player);

        if (player != null)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            StartCoroutine(DestroyCoin());
        }
    }

    private IEnumerator DestroyCoin()
    {
        float delay = 0.1f;

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
