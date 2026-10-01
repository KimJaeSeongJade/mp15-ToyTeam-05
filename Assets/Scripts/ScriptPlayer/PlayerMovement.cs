using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _moveSpeed = 1000f;
    private float _rotSpeed = 500f;

    [SerializeField] private bool _IsPlayer1p;

    private void Awake() => CacheComponents();
    
    public void Start()
    {
        SetEvents(_IsPlayer1p);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInputP1 -= Move;
        InputManager.Instance.OnInputP1 -= Rotate;
        InputManager.Instance.OnInputP2 -= Move;
        InputManager.Instance.OnInputP2 -= Rotate;
    }

    private void SetEvents(bool isPlayer1p)
    {
        if (isPlayer1p)
        {
            InputManager.Instance.OnInputP1 += Move;
            InputManager.Instance.OnInputP1 += Rotate;
        }
        else
        {
            InputManager.Instance.OnInputP2 += Move;
            InputManager.Instance.OnInputP2 += Rotate;
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
