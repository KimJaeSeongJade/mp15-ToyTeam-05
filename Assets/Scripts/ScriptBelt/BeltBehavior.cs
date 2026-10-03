using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltBehavior : MonoBehaviour
{
    private readonly int _itemLayerMask = 12;

    // 상수 / Readonly 필드
    // ============================================================
    
    private List<Food> _foods = new();
    private Vector3 _beltDirection;
    private float _beltPower = 8f;
    
    // 인스턴스 필드
    // ============================================================

    private void Awake()
    {
        _beltDirection = transform.forward;
    }

    private void LateUpdate()
    {
        MoveRail();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == _itemLayerMask)
        {
            _foods.Add(collider.GetComponent<Food>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_foods.Contains(collider.GetComponent<Food>()))
        {
            _foods.Remove(collider.GetComponent<Food>());
        }
    }
    
    // 이벤트 함수
    // ============================================================

    private void MoveRail()
    {
        _foods.RemoveAll(food => food == null);

        if (_foods.Count <= 0) return;
        foreach (Food food in _foods)
        {
            food.FoodRigidbody.velocity = Vector3.zero;
            food.FoodRigidbody.AddForce(_beltDirection.normalized * _beltPower);
        }
    }
    
    // 비공개 메서드
    // ============================================================
    
    
}
