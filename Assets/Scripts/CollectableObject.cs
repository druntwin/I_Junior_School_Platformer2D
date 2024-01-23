using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _destroyObjectDelay = 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out Player player);

        if (player != null)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            StartCoroutine(DestroyObject());
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(_destroyObjectDelay);

        Destroy(gameObject);
    }
}
