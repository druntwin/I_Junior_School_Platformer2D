using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;

    private Transform[] _points;
    private int _currentPointIndex;

    private void Start()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i);
        }

        SpawnObject();
    }

    private void SpawnObject()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_objectPrefab, _points[i].transform.position, Quaternion.identity, _points[i].transform);
        }
    }
}
