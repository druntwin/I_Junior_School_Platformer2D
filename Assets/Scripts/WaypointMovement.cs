using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _path;
    [SerializeField][Range(0f, 5f)] private float _speed = 0.5f;
    [SerializeField] private LayerMask _rayCastLayerMask;
    [SerializeField] private float _rayCastDistance = 5f;

    private Transform[] _points;
    private int _currentPointIndex;
    private float _characterDirection;    

    private void Start()
    {
        _characterDirection = 0f;

        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        MoveByWaypoints();
    }

    private void MoveByWaypoints()
    {
        Transform target;
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, Vector2.right * new Vector2(_characterDirection, 0f), _rayCastDistance, _rayCastLayerMask);

        if (hitObstacle.collider != null)
        {
            target = hitObstacle.transform;
            Debug.DrawRay(transform.position, Vector2.right * new Vector2(_characterDirection, 0f) * hitObstacle.distance, Color.red);
            Debug.Log(hitObstacle.collider.name);
        }
        else
        {
            target = _points[_currentPointIndex];
            Debug.DrawRay(transform.position, Vector2.right * new Vector2(_characterDirection, 0f) * hitObstacle.distance, Color.green);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position.x < target.position.x)
        {
            _spriteRenderer.flipX = false;
            _characterDirection = 1f;
        }

        if (transform.position.x > target.position.x)
        {
            _spriteRenderer.flipX = true;
            _characterDirection = -1f;
        }

        if (transform.position == target.position)
        {
            _currentPointIndex++;

            if (_currentPointIndex >= _points.Length)
            {
                _currentPointIndex = 0;
            }
        }
    }
}
