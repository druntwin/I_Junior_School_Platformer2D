using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        if (_player != null)
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}
