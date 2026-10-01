using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _moveSpeed = 1000f;
    private float _rotSpeed = 500f;

    private void Awake() => CacheComponents();
    
    private void Start()
    {
        InputManager.Instance.OnInputP1 += Move;
        // InputManager.Instance.OnInputP1 += Rotate;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInputP1 -= Move;
        // InputManager.Instance.OnInputP1 -= Rotate;
    }

    private void Move(Vector3 dir)
    {
        _rigidbody.AddForce(dir * _moveSpeed * Time.deltaTime, ForceMode.Force);
    }

    private void Rotate(Vector3 dir)
    {
        Quaternion rot = Quaternion.Euler(dir);
        transform.forward = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * _rotSpeed).eulerAngles;
    }

    private void Update()
    {
        transform.forward = Vector3.Slerp(transform.forward, _rigidbody.velocity, Time.deltaTime * _moveSpeed);
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
