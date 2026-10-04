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
    private char _recipeChar; // 추후 레시피 리팩토링하면 변경
    
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
                case CookwareJobEnum.Idle:
                    _cookProgress = CookwareJobEnum.Start;
                    _recipeChar = _foodData.FoodId[1];
                    break;
                case CookwareJobEnum.ThreeFourth:
                    yield return _choppingTime;
                    if (_isStillHoldKey)
                    {
                        RemoveFood(_foodData);
                        FoodProcess();
                        _recipeChar = 'N';
                        _cookProgress = CookwareJobEnum.Idle;
                        _isStillHoldKey = false;
                    }
                    break;
                default:
                    yield return _choppingTime;
                    if (_isStillHoldKey) _cookProgress = NextJobEnum(_cookProgress);
                    break;
            }
            Debug.Log($"요리 진행도 : {(int)_cookProgress}");
        }
        _playerInteractor = null;
    }
    
    
    // 코루틴
    // ============================================================
    

    private void OnTriggerEnter(Collider other)
    {
        ColliderEnterCheck(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ColliderExitCheck(other);
    }
    
    private void Update()
    {
        CheckPress();
    }
    
    // 이벤트 함수
    // ============================================================
    
    public override void Interact(IInteractor interactor)
    {
        if (!CanWork()) return;
        _playerInteractor = interactor;
        IsPressed = interactor.IsPressed;
        StartCoroutine(_choppingCoroutine());
    }

    public override void Interact(IInteractor interactor, IHoldable holdable)
    {
        if (!CanWork(holdable)) return;
        _playerInteractor = interactor;
        holdable.Release();
        holdable.Hold(this);
        StartCoroutine(_choppingCoroutine());
    }
    
    public override bool CanWork()
    {
        if (_foodData == null) return false;
        return CheckRecipe(_foodData);
    }

    public override bool CanWork(IHoldable holdable)
    {
        if (_foodData != null) return false;
        return CheckRecipe(holdable.FoodData);
    }
    
    // 공개 메서드
    // ============================================================
    
    private bool CheckRecipe(Food food)
    {
        if (food == null) return false;
        if (food.FoodId == "01") return true;
        if (food.FoodId == "03") return true;
        return false;
    }

    private void FoodProcess()
    {
        if (_recipeChar == '1')
        {
            _foodData = Instantiate(ChoppedLettuce, TargetTransform);
            return;
        }
        if (_recipeChar == '3')
        {
            _foodData = Instantiate(ChoppedTomato, TargetTransform);
            return;
        }
        
    }

    private void RemoveFood(Food food)
    {
        Destroy(food.gameObject);
        UnSetFood();
    }

    private void CheckPress()
    {
        if (!_isStillHoldKey) return;
        if (!_playerInteractor.IsPressed) _isStillHoldKey = false;
    }
    
    
    // 비공개 메서드
    // ============================================================
}
