using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingPot : Cookware
{
    [SerializeField] private Food TomatoSauce;
    [SerializeField] private Food OnionStock;
    
    // 시리얼라이즈 필드
    // ============================================================
    
    private bool _isCooking => _cookProgress >= (int)CookwareJobEnum.Start;
    private bool _isWaitingTimer;
    private bool _readyToPick;
    [SerializeField] private float _cookingFloat;
    private int _cookingSpeed = 10; // 1 = 100초 / 5 = 20초;
    private Vector3 _shrink = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 _normalize = new Vector3(1, 1, 1);
    
    // 비공개 필드
    // ============================================================

    private void Update()
    {
        if (_isCooking) Boil(); 
    }
    
    private void OnTriggerStay(Collider collision)
    {
        CheckTrigger(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        OutTrigger(collision);
    }
    
    // 이벤트 함수
    // ============================================================
    

    protected override bool CheckRecipe(Food food)
    {
        Debug.Log($"레시피 확인 {food.FoodId}");
        if (food == null) return false;
        if (food.FoodId == "03") return true;
        if (food.FoodId == "08") return true;
        return false;
    }

    protected override void StartCooking()
    {
        Debug.Log("냄비 시도");
        if (_cookProgress == -1) StartBoil();
    }

    // 공개 메서드
    // ============================================================

    private void StartBoil()
    {
        _lastHolder = _foodData.LastHolder;
        _cookProgress = (int)CookwareJobEnum.Start;
        _recipeChar = _foodData.FoodId[1];
        _readyToPick = false;
    }

    private void Boil()
    {
        if (_cookProgress == -1) return;
        if (_cookProgress < 100)
        {
            _foodData.CanHolding = false;
            _cookingFloat += Time.deltaTime * _cookingSpeed;
        }
        else
        {
            RemoveFood(_foodData);
            FoodProcess();
            Debug.Log(_cookingFloat);
        }
        _cookProgress = (int)_cookingFloat;
        Debug.Log($"냄비 진행도 : {_cookProgress}");
    }

    private void FoodProcess()
    {
        if (_recipeChar == '3')
        {
            Food ob = PoolManager.Instance.Get(TomatoSauce);
            ob.Hold(this, PLAYER_ID.NONE);
            ob.ActiveThisFood();
        }
        if (_recipeChar == '8')
        {
            Food ob = PoolManager.Instance.Get(OnionStock);
            ob.Hold(this, PLAYER_ID.NONE);
            ob.ActiveThisFood();
        }
        _playerInteractor = null;
        _readyToPick = true;
        _recipeChar = 'N';
        _cookingFloat = -1;
    }
    // 비공개 메서드
    // ============================================================
}
