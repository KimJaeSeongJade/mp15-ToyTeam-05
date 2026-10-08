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
    private float _cookingFloat;
    private int _cookingSpeed = 10; // 1 = 100초 / 5 = 20초;
    
    // 비공개 필드
    // ============================================================

    private void Update()
    {
        CheckCookStatus();
        GrabFood();
        if (_isCooking) Boil();
    }
    
    private void OnTriggerEnter(Collider collision)
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
        if (food == null) return false;
        if (food.FoodId == "03") return true;
        if (food.FoodId == "08") return true;
        return false;
    }

    protected override void StartCooking()
    {
        if (_cookProgress == -1 
            && CheckRecipe(_currentFood)) StartBoil();
    }

    // 공개 메서드
    // ============================================================

    private void StartBoil()
    {
        SoundManager.Instance.SFXPlay(SFXType.Pot);
        _playerHolder = _currentFood.LastHolder;
        _cookProgress = (int)CookwareJobEnum.Start;
        _currentFood.CanHolding = false;
        _recipeChar = _currentFood.FoodId[1];
    }

    private void Boil()
    {
        if (_cookProgress == -1) return;
        if (_cookProgress < 100)
        {
            _cookingFloat += Time.deltaTime * _cookingSpeed;
        }
        else
        {
            RemoveFood(_currentFood);
            FoodProcess();
        }
        _cookProgress = (int)_cookingFloat;
    }

    private void FoodProcess()
    {
        SoundManager.Instance.SFXPlay(SFXType.Dish);
        if (_recipeChar == '3')
        {
            Food ob = PoolManager.Instance.Get(TomatoSauce);
            ob.ActiveCookFood();
            SetFood(ob);
        }
        if (_recipeChar == '8')
        {
            Food ob = PoolManager.Instance.Get(OnionStock);
            ob.ActiveCookFood();
            SetFood(ob);
        }
        _playerInteractor = null;
        _recipeChar = 'N';
        _cookingFloat = -1;
        _cookProgress = (int)_cookingFloat;
    }
    // 비공개 메서드
    // ============================================================
}
