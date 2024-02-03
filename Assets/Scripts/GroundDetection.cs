using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 1.3f;
    private int _layerMask = (int)LayerMasks.Ground;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position - new Vector3(0,_rayDistance,0), Color.green);

        if (Physics2D.Raycast(transform.position, -Vector2.up, _rayDistance, _layerMask))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }  
}
