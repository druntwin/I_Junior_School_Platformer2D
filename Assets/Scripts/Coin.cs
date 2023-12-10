using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out PlayerController playerController);

        if (playerController != null)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            StartCoroutine(DestroyCoin());
        }
    }

    IEnumerator DestroyCoin()
    {
        float delay = 0.1f;
        var waitSomeTime = new WaitForSeconds(delay);

        yield return waitSomeTime;

        Destroy(gameObject);
    }
}
