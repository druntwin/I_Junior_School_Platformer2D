using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private Transform[] _points;
    private int _currentPointIndex;

    private void Start()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i);
        }

        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_coinPrefab, _points[i].transform.position, Quaternion.identity, _points[i].transform);
        }
    }
}
