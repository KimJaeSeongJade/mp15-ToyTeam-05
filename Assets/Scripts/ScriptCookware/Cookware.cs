using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHolder
{
    [SerializeField] protected Transform _targetTransform;
    
    protected int _playerLayer = 6;
    protected int _foodLayer = 12;
    protected char _recipeChar;
    
    [SerializeField] protected int _cookProgress = (int)CookwareJobEnum.Idle;
    [SerializeField] protected IInteractor _playerInteractor;
    [SerializeField] protected IHolder _lastHolder;

    [SerializeField] protected bool _canHoldItem;
    [SerializeField] protected bool _canRealeaseItem;
    
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

    public void Interact(IInteractor interactor)
    {
        if (!CanWork()) return;
        _playerInteractor = interactor;
        IsPressed = interactor.IsPressed;
        StartCooking();
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        if (!CanWork(holdable)) return;
        _playerInteractor = interactor;
        holdable.Release();
        SetFood(holdable.FoodData);
        StartCooking();
    }

    protected abstract void StartCooking();
    
    protected abstract bool CheckRecipe(Food food);
    
    // 추상 메서드
    // ============================================================

    private void Update()
    {
        CheckCookStatus();
    }
        
    // 이벤트 함수
    // ============================================================
    
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

    protected void CheckTrigger(Collider collision)
    {
        if (IsHolding) return;
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            Food food = collision.gameObject.GetComponent<Food>();
            SetFood(food);
        }
    }

    protected void OutTrigger(Collider collision)
    {
        if (!IsHolding) return;
        if (collision.gameObject.GetComponent<Food>() == _foodData) UnSetFood();
    }
    
    /// <summary>
    /// 해당 음식을 등록
    /// </summary>
    /// <param name="food">등록할 음식</param>
    protected void SetFood(Food food)
    {
        _foodData = food;
            
        _foodData.transform.position = TargetTransform.position;
        _foodData.transform.rotation = TargetTransform.rotation;
        _foodData.Hold(this, PLAYER_ID.NONE);
    }
    
    /// <summary>
    /// 등록된 음식값을 없앰
    /// </summary>
    protected void UnSetFood()
    {
        _foodData = null;
    }

    // 공개 메서드
    // ============================================================

    
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
