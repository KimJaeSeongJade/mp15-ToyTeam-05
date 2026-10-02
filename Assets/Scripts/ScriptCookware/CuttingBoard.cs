using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    private bool _isWorking;
    private WaitForSeconds _choppingTime = new  WaitForSeconds(0.4f);
    
    // 비공개 필드
    // ============================================================
    
    
    // 프로퍼티
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
    
    public override void Interact(IInteractor interactor)
    {
        Debug.Log("상호작용");
    }

    public override void Interact(IInteractor interactor, IHoldable holdable)
    {
        
    }
    
    public override bool CanWork()
    {
        if (_foodData == null) return false;
        return CheckRecipe();
    }

    public override bool CanWork(IHoldable holdable)
    {
        if (_foodData != null) return false;

        return false;
    }
    
    // 공개 메서드
    // ============================================================

    private bool CheckRecipe()
    {
        if (_foodData == null) return false;
        if (_foodData.FoodId == "01") return true;
        if (_foodData.FoodId == "03") return true;
        return false;
    }
    
    
    
    // 비공개 메서드
    // ============================================================
}
