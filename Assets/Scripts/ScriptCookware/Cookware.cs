using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHolder
{
    public Transform TransformInteract { get; private set; }
    
    [SerializeField] protected Transform _targetTransform;
    
    protected int _playerLayer = 6;
    protected int _foodLayer = 12;
    protected char _recipeChar;
    
    protected float _cookProgress = (float)CookwareJobEnum.Idle;
    protected IInteractor _playerInteractor;
    protected IHolder _playerHolder;

    protected bool _canRealeaseItem;

    [SerializeField] protected List<Food> _foods = new();
    [SerializeField] protected Food _currentFood;
    
    // 비공개 필드
    // ============================================================

    public void SetTransform(Transform tr)
    {
        this.TransformInteract = tr;
    }

    public Transform TargetTransform => _targetTransform;

    public bool CanHold => _currentFood == null;
    public bool CanRelease => _canRealeaseItem;
    public bool IsPressed { get; set; }
    
    public Food FoodData => _currentFood;

    public bool IsHolding => _currentFood != null;
    public bool IsCooking => _cookProgress != (int)CookwareJobEnum.Idle;
    
    public float CookProgress => _cookProgress;
    
    // 프로퍼티
    // ============================================================

    public void Interact(IInteractor interactor)
    {
        if (!CanWork()) return;
        _playerInteractor = interactor;
        StartCooking();
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        if (!CanWork(holdable)) return;
        holdable.Release();
    }

    protected abstract void StartCooking();
    
    protected abstract bool CheckRecipe(Food food);
    
    // 추상 메서드
    // ============================================================
    
    public bool CanWork()
    {
        if (!IsHolding) return false;
        return CheckRecipe(_currentFood);
    }

    public bool CanWork(IHoldable holdable)
    {
        if (IsHolding) return false;
        return CheckRecipe(holdable.FoodData);
    }
    
    public void RemoveData(Food food)
    {
        PlayerPickup(food);
        if (_foods.Contains(food))
        {
            _foods.Remove(food);
            food.OnReturnPool -= RemoveData;
        }
    }
    
    // public 메서드
    // ============================================================

    protected void CheckTrigger(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            _foods.Add(food);
            food.OnReturnPool += RemoveData;
        }
    }

    protected void OutTrigger(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            RemoveData(food);
        }
    }

    protected void GrabFood()
    {
        if (!CanHold||_foods.Count == 0) return;
        foreach (Food food in _foods)
        {
            if (!CheckRecipe(food)) continue;
            SetFood(food);
        }
    }
    
    protected void SetFood(Food food)
    {
        if (!food.CanHolding) return;
        _currentFood = food;
        food.Hold(this, PLAYER_ID.NONE);
        food.OnPlayerHold += PlayerPickup;
    }

    protected void UnSetFood()
    {
        _currentFood = null;
    }
    
    protected void RemoveFood(Food food)
    {
        food.ReturnToPool();
        PlayerPickup(food);
    }
    
    /// <summary>
    /// 요리중일때 음식을 못집어 올리게 bool값을 설정하는 함수
    /// </summary>
    protected void CheckCookStatus()
    {
        if (!IsHolding) _cookProgress = (int)CookwareJobEnum.Idle;
        else _currentFood.CanHolding = !IsCooking;
    }
    protected void PlayerPickup(Food food)
    {
        if (food == _currentFood)
        {
            UnSetFood();
            food.OnPlayerHold -= PlayerPickup;
        }
    }
    
    
    // protected 메서드
    // ============================================================
    
    
    // private 메서드
    // ============================================================
}
