using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : Cookware
{
    [SerializeField] private Food ChoppedLettuce;
    [SerializeField] private Food ChoppedTomato;
    
    // 시리얼라이즈 필드
    // ============================================================
    
    private bool _isCooking;
    private bool _isWaitingTimer;
    private bool _isStillHoldKey;
    
    private WaitForSeconds _choppingTime = new  WaitForSeconds(0.5f);
    
    // 비공개 필드
    // ============================================================
    
    
    // 프로퍼티
    // ============================================================

    private IEnumerator _choppingCoroutine()
    {
        _isStillHoldKey = true;
        while (_isStillHoldKey)
        {
            switch (_cookProgress)
            {
                case (int)CookwareJobEnum.Idle:
                    _lastHolder = _foodData.LastHolder;
                    _cookProgress = (int)CookwareJobEnum.Start;
                    _recipeChar = _foodData.FoodId[1];
                    _foodData.CanHolding = false;
                    break;
                case (int)CookwareJobEnum.ThreeFourth:
                    yield return _choppingTime;
                    if (_isStillHoldKey)
                    {
                        RemoveFood(_foodData);
                        FoodProcess();
                    }
                    break;
                default:
                    yield return _choppingTime;
                    if (_isStillHoldKey) NextChop();
                    break;
            }
            Debug.Log($"요리 진행도 : {(int)_cookProgress}");
        }
        _playerInteractor = null;
    }
    
    
    // 코루틴
    // ============================================================
    
    private void OnTriggerStay(Collider collision)
    {
        CheckTrigger(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        OutTrigger(collision);
    }
    
    private void Update()
    {
        CheckPress();
    }
    
    // 이벤트 함수
    // ============================================================

    protected override void StartCooking()
    {
        IsPressed = _playerInteractor.IsPressed;
        StartCoroutine(_choppingCoroutine());
    }

    // 공개 메서드
    // ============================================================
    
    protected override bool CheckRecipe(Food food)
    {
        Debug.Log($"레시피 확인 {food.FoodId}");
        if (food == null) return false;
        if (food.FoodId == "01") return true;
        if (food.FoodId == "03") return true;
        return false;
    }

    private void FoodProcess()
    {
        if (_recipeChar == '1')
        {
            Food ob = PoolManager.Instance.Get(ChoppedLettuce);
            ob.ActiveCookFood();
            ob.Hold(this, PLAYER_ID.NONE);
        }
        if (_recipeChar == '3')
        {
            Food ob = PoolManager.Instance.Get(ChoppedTomato);
            ob.ActiveCookFood();
            ob.Hold(this, PLAYER_ID.NONE);
        }
        _recipeChar = 'N';
        _cookProgress = (int)CookwareJobEnum.Idle;
        _isStillHoldKey = false;
        Debug.Log(_foodData);
        UnSetFood();
    }

    private void CheckPress()
    {
        if (!_isStillHoldKey) return;
        if (!_playerInteractor.IsPressed) _isStillHoldKey = false;
    }

    private void NextChop()
    {
        _cookProgress += 25;
    }
    
    
    // 비공개 메서드
    // ============================================================
}
