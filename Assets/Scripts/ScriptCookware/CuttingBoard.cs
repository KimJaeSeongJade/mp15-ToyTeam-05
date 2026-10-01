using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    
    private readonly int _playerLayerMask = (1 << 6);

    private IHoldable _holdableItem;
    
    private IInteractor _playerInteractor;
    private Rigidbody _rigidbody;
    
    // 상수 / Readonly 필드
    // ============================================================
    
    // 인스턴스 필드
    // ============================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayerMask)
        {
            _playerInteractor = other.GetComponent<IInteractor>();
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
    public override Rigidbody Rigidbody => _rigidbody;
    public override Transform TargetTransform { get; }
    public override bool IsHolding { get; }

    // 프로퍼티
    // ============================================================

    public override string GetData()
    {
        return (_holdableItem.GetData());
    }

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

    private void Cutting()
    {
    }
    
    // 비공개 메서드
    // ============================================================
}
