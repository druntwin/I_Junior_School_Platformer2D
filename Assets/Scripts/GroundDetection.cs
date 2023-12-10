using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1.3f;

    // Bit shift the index of the layer (8) to get a bit mask
    private int layerMask = 1 << 8;

    public bool IsGrounded { get; private set; }


    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position - new Vector3(0,rayDistance,0), Color.green);

        if (Physics2D.Raycast(transform.position, -Vector2.up, rayDistance, layerMask))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }  
}
