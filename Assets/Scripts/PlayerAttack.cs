using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _rayDistance = 1.3f;
    [SerializeField] private int _damage = 0;

    private int _layerMask = (int)LayerMasks.Enemies;
    private bool _isHit = false;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _rayDistance, _layerMask);

        if (hit.collider != null)
        {
            hit.collider.transform.TryGetComponent(out Health health);

            if (health != null)
            {
                if(_isHit == false)
                {
                    health.Reduse(_damage);
                    _player.DoAttackJump();
                    _isHit = true;
                }
            }            
        }
        else
        {
            _isHit = false;
        }
    }
}
