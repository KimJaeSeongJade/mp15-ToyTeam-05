using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _moveSpeed = 1000f;
    private float _rotSpeed = 500f;

    [SerializeField] public PLAYER_ID _playerID;

    private void Awake() => CacheComponents();
    
    public void Start()
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
    
    private void Move(Vector3 dir)
    {
        _rigidbody.AddForce(dir * _moveSpeed * Time.deltaTime, ForceMode.Force);
    }

    private void Rotate(Vector3 dir)
    {
        transform.forward += Vector3.Slerp(transform.forward, _rigidbody.velocity, Time.deltaTime * _rotSpeed);
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
