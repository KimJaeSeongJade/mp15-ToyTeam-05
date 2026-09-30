using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable, IHolder
{
    
    
    // 인스턴스 필드
    // ============================================================
    
    public bool IsHolding { get; }
    public bool CanHold { get; }
    public bool CanRelease { get; }
    
    // 프로퍼티
    // ============================================================
    
    
    public void Interact(IInteractor interactor)
    {
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
    }

    public void Hold(IHolder holder)
    {
    }

    public void Release()
    {
    }
    
    // 메서드
    // ============================================================
}
