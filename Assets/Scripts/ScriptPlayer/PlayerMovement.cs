using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _moveSpeed = 300f;
    private float _rotSpeed = 20f;

    [SerializeField] public PLAYER_ID _playerID;

    private void Awake() => CacheComponents();
    
    public void OnEnable()
    {
        BindInputEvents();
    }

    private void OnDisable()
    {
        UnBindInputEvents();
    }

    private void BindInputEvents()
    {
        switch (_playerID)
        {
            case PLAYER_ID.PLAYER_1P:
                InputManager.Instance.OnInputP1 += Move;
                InputManager.Instance.OnInputP1 += Rotate;
                break;
            case PLAYER_ID.PLAYER_2P:
                InputManager.Instance.OnInputP2 += Move;
                InputManager.Instance.OnInputP2 += Rotate;
                break;
        }
    }
    
    private void UnBindInputEvents()
    {
        switch (_playerID)
        {
            case PLAYER_ID.PLAYER_1P:
                InputManager.Instance.OnInputP1 -= Move;
                InputManager.Instance.OnInputP1 -= Rotate;
                break;
            case PLAYER_ID.PLAYER_2P:
                InputManager.Instance.OnInputP2 -= Move;
                InputManager.Instance.OnInputP2 -= Rotate;
                break;
        }
    }

    public void ChangeSpeed(float speed)
    {
        _moveSpeed = speed;
    }
    
    private void Move(Vector3 dir)
    {
        _rigidbody.velocity = Vector3.zero;
        Vector3 movement = (dir * _moveSpeed * Time.deltaTime);
        
        _rigidbody.AddForce(movement, ForceMode.Impulse);
    }

    private void Rotate(Vector3 dir)
    {
        if (dir.magnitude < 0.1f) return;
        
        Quaternion rotation = Quaternion.LookRotation(dir);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotSpeed);
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
