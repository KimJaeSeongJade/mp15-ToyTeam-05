using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    
    private readonly int _playerLayerMask = (1 << 6);
    private readonly int _ItemLayerMask = (1 << 12);

    
    // 상수 / Readonly 필드
    // ============================================================
    
    private IInteractor _playerInteractor;
    private Rigidbody _rigidbody;
    private IHoldable _holdFood;

    private bool _isHaveItem;
    
    // 인스턴스 필드
    // ============================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayerMask)
        {
            _playerInteractor = other.GetComponent<IInteractor>();
        }
        else if (other.gameObject.layer == _ItemLayerMask && !_isHaveItem)
        {
            _holdFood = other.GetComponent<IHoldable>();
            SetItemOnBoard();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayerMask)
        {
            _playerInteractor = null;
        }
    }
    
    // 이벤트
    // ============================================================

    public override bool CanHolding { get; }
    
    public override bool CanHold { get; }
    
    public override bool CanRelease { get; }
    public override Rigidbody FoodRigidbody => _rigidbody;
    public override Transform FoodTransform => _holdFood.FoodTransform;

    public override Transform TargetTransform { get; } 
    public override bool IsHolding { get; } // 음식을 가지고 있는지
    public override Food FoodData { get; } // 음식 데이터

    // 프로퍼티
    // ============================================================


    public override void Interact(IInteractor interactor)
    {
        if (!interactor.CanInteract) return;
    }

    public override void Interact(IInteractor interactor, IHoldable holdable)
    {
        if (!interactor.CanInteract) return;
        
    }

    public override void Hold(IHolder holder)
    {
        
    }

    public override void Release()
    {
    }
    
    // 공개 메서드
    // ============================================================

    private void SetItemOnBoard()
    {
        _holdFood.FoodTransform.rotation = Quaternion.Euler(Vector3.zero);
        _holdFood.FoodTransform.position = TargetTransform.position;
    }

    private void Cutting()
    {
        // 들어온 아이템을 일정 시간후에 잘린 상태로 반환 
    }
    
    // 비공개 메서드
    // ============================================================
}
