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
    private char _recipeChar;
    
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
                    _cookProgress = (int)CookwareJobEnum.Start;
                    _recipeChar = _foodData.FoodId[1];
                    break;
                case (int)CookwareJobEnum.ThreeFourth:
                    yield return _choppingTime;
                    if (_isStillHoldKey)
                    {
                        RemoveFood(_foodData);
                        FoodProcess();
                        _recipeChar = 'N';
                        _cookProgress = (int)CookwareJobEnum.Idle;
                        _isStillHoldKey = false;
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
        holdable.Hold(this, PLAYER_ID.NONE);
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
            // ----------------- 변경 전
            //_foodData = Instantiate(ChoppedLettuce, TargetTransform.position, Quaternion.identity);
            // ----------------- 변경 전
            
            // ----------------- 변경
            Food ob = PoolManager.Instance.Get(ChoppedLettuce);
        
            ob.transform.position = TargetTransform.position;
            ob.transform.rotation = Quaternion.identity;
            ob.gameObject.SetActive(true);
            // ----------------- 변경
            return;
        }
        if (_recipeChar == '3')
        {
            //_foodData = Instantiate(ChoppedTomato, TargetTransform.position, Quaternion.identity);
            
            // ----------------- 변경
            Food ob = PoolManager.Instance.Get(ChoppedTomato);
        
            ob.transform.position = TargetTransform.position;
            ob.transform.rotation = Quaternion.identity;
            ob.gameObject.SetActive(true);
            // ----------------- 변경
            
            return;
        }
        
    }

    private void RemoveFood(Food food)
    {
        _lastHolder.RemoveData(food);
        //Destroy(food.gameObject);
        // ------------------ 변경
        // 플레이어 잡을 수 있는 리스트에서 빼줘야함
        food.CanHolding = false;
        food.Release();
        food.gameObject.SetActive(false);
        // ------------------ 변경
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
