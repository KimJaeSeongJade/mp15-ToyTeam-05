using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    
    // 인스턴스 필드
    // ============================================================
    
    
    // ============================================================
    
    


    // 이벤트
    // ============================================================
    
    
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
}
