using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltBehavior : MonoBehaviour
{
    private readonly int _itemLayerMask = 12;

    // 상수 / Readonly 필드
    // ============================================================
    
    private List<IHoldable> _items = new();
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
            _items.Add(collider.GetComponent<IHoldable>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_items.Contains(collider.GetComponent<IHoldable>()))
        {
            _items.Remove(collider.GetComponent<IHoldable>());
        }
    }
    
    // 이벤트 함수
    // ============================================================

    private void MoveRail()
    {
        foreach (IHoldable item in _items)
        {
            item.FoodRigidbody.velocity = Vector3.zero;
            item.FoodRigidbody.AddForce(_beltDirection.normalized * _beltPower);
        }
    }
    
    // 비공개 메서드
    // ============================================================
    
    
}
