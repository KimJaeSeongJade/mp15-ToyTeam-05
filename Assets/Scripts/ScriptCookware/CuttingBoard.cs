using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    
    
    // 상수 / Readonly 필드
    // ============================================================
    
    [SerializeField] Transform HoldingTransform;
    private IInteractor _playerInteractor;
    private Food _holdFood;
    
    private bool _isHaveItem => _holdFood != null;
    
    // 인스턴스 필드
    // ============================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayerMask)
        {
            _playerInteractor = other.GetComponent<IInteractor>();
        }
        else if (other.gameObject.layer == _itemLayerMask && !_isHaveItem)
        {
            _holdFood = other.GetComponent<Food>();
            SetFood(_holdFood);
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


    private void SetFood(Food food)
    {
        _holdFood = food;
        
    }
    
    // 비공개 메서드
    // ============================================================
}
