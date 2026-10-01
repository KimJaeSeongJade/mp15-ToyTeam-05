using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    
    
    // 비공개 필드
    // ============================================================

    private void OnTriggerEnter(Collider other)
    {
        ColliderEnterCheck(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ColliderExitCheck(other);
    }
    
    // 이벤트 함수
    // ============================================================
   

    public override void Release()
    {
        Debug.Log("놓아");
    }

    public override void Interact(IInteractor interactor)
    {
        Debug.Log("상호작용");
    }

    public override void Interact(IInteractor interactor, IHoldable holdable)
    {
        Debug.Log("물건들고 상호작용");
    }
    
    // 공개 메서드
    // ============================================================
    
    
    
    // 비공개 메서드
    // ============================================================
    
}
