using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltBehavior : MonoBehaviour
{
    // private readonly int _itemLayerMask = 12;

    // 상수 / Readonly 필드
    // ============================================================
    
    private Vector3 _beltDirection;
    private float _beltPower = 70f; // 벨트 속도
    
    // 인스턴스 필드
    // ============================================================

    private void Awake()
    {
        _beltDirection = transform.forward;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out Food food)) return;
        MoveRail(food);
    }

    // 이벤트 함수
    // ============================================================

    private void MoveRail(Food food)
    {
        food.FoodRigidbody.velocity = Vector3.zero;
        food.FoodRigidbody.AddForce(_beltDirection * (_beltPower * Time.deltaTime), ForceMode.Impulse);
    }
    
    // 비공개 메서드
    // ============================================================
    
    
}
