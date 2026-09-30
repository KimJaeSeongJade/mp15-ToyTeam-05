using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookware : MonoBehaviour, IInteractable, IHoldable, IHolder
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
        if (!interactor.CanInteract) return;
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        if (!interactor.CanInteract) return;
    }

    public void Hold(IHolder holder)
    {
        if (!holder.CanHold) return;
    }

    public void Release()
    {
        
    }
    
    // 메서드
    // ============================================================
}
