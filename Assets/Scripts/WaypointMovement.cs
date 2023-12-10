using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _path;
    [SerializeField][Range(0f, 5f)] private float _speed = 0.5f;

    private Transform[] _points;
    private int _currentPointIndex;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position.x < target.position.x)
            _spriteRenderer.flipX = false;

        if (transform.position.x > target.position.x)
            _spriteRenderer.flipX = true;

        if (transform.position == target.position)
        {
            _currentPointIndex++;

            if( _currentPointIndex >= _points.Length)
            {
                _currentPointIndex = 0;
            }
        }
    }
}
