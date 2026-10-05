using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingPot : Cookware
{

    private bool _isCooking => _cookProgress >= (int)CookwareJobEnum.Start;
    private bool _isWaitingTimer;
    private bool _isStillHoldKey;
    private float _cookingFloat;
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
        if (food.FoodId == "05") return true;
        if (food.FoodId == "10") return true;
        return false;
    }

    protected override void StartCooking()
    {
        StartBoil();
    }

    // 공개 메서드
    // ============================================================

    private void StartBoil()
    {
        _cookProgress = (int)CookwareJobEnum.Start;
        _recipeChar = _foodData.FoodId[1];
        _foodData.gameObject.transform.localScale = _shrink;
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
            _foodData.Release();
            // 오브젝트 풀로 반환
            ProcessFood();
        }
        _cookProgress = (int)_cookingFloat;
    }

    private void ProcessFood()
    {
        if (_recipeChar == '5')
        {
            // 오브젝트풀에서 꺼냄
            return;
        }
        if (_recipeChar == '0')
        {
            // 오브젝트풀에서 꺼냄
            return;
        }
        _recipeChar = 'N';
        _cookingFloat = -1;
    }
    // 비공개 메서드
    // ============================================================
}
