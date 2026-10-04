using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHolder
{
    [SerializeField] protected Transform _targetTransform;
    
    protected int _playerLayer = 6;
    protected int _foodLayer = 12;
    protected char _recipeChar;
    
    protected int _cookProgress = (int)CookwareJobEnum.Start;
    protected IInteractor _playerInteractor;
    protected IHolder _lastHolder;

    protected bool _canHoldItem;
    protected bool _canRealeaseItem;
    
    [SerializeField] protected Food _foodData;
    
    // 비공개 필드
    // ============================================================


    public Transform TargetTransform => _targetTransform;
    
    public bool CanHold => _canHoldItem;
    public bool CanRelease => _canRealeaseItem;
    public bool IsPressed { get; set; }
    
    public Food FoodData => _foodData;

    public bool IsHolding => _foodData != null;
    public bool IsCooking => _cookProgress != (int)CookwareJobEnum.Idle;
    
    public int CookProgress => _cookProgress;
    
    // 프로퍼티
    // ============================================================

    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);
    
    protected abstract bool CheckRecipe(Food food);
    
    // 추상 메서드
    // ============================================================

    private void Update()
    {
        CheckCookStatus();
    }
        
    // 이벤트 함수
    // ============================================================
    
    
    protected void ColliderEnterCheck(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
        }
        else if (other.TryGetComponent(out Food food)
                 && !IsHolding)
        {
            if (!food.CanHolding) return;
            SetFood(food);
            _lastHolder = food.LastHolder;
            food.Hold(this, PLAYER_ID.NONE);
        }
    }

    protected void ColliderExitCheck(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
        }
        else if (other.gameObject.layer == _foodLayer
                 && other.TryGetComponent(out Food food))
        {
            if (food != _foodData) return;
            _foodData.Release();
            UnSetFood();
        }
    }

    /// <summary>
    /// 해당 음식을 등록
    /// </summary>
    /// <param name="food">등록할 음식</param>
    protected void SetFood(Food food)
    {
        _foodData = food;
    }

    /// <summary>
    /// 등록된 음식값을 없앰
    /// </summary>
    protected void UnSetFood()
    {
        _foodData = null;
    }
    
    public bool CanWork()
    {
        if (!IsHolding) return false;
        return CheckRecipe(_foodData);
    }

    public bool CanWork(IHoldable holdable)
    {
        if (IsHolding) return false;
        return CheckRecipe(holdable.FoodData);
    }

    public void RemoveData(Food food)
    {
        if (_foodData == food) UnSetFood();
    }

    // 공개 메서드
    // ============================================================

    /// <summary>
    /// 넣은 음식의 ID값을 반환
    /// </summary>
    /// <param name="food">ID값을 알고 싶은 음식</param>
    /// <returns></returns>
    private string GetFoodID(Food food)
    {
        return food.FoodId;
    }
    
    /// <summary>
    /// 요리중일때 음식을 못집어 올리게 bool값을 설정하는 함수
    /// </summary>
    private void CheckCookStatus()
    {
        if (!IsHolding) _cookProgress = (int)CookwareJobEnum.Idle;
        else FoodData.CanHolding = !IsCooking;
    }
    
    // private 메서드
    // ============================================================
}
