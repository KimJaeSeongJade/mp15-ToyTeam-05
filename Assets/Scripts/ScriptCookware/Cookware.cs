using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHolder
{
    [SerializeField] protected Transform _targetTransform;
    
    protected int _playerLayer = 6;
    protected int _foodLayer = 12;
    protected CookwareJobEnum _cookProgress;

    protected bool _canHoldItem;
    protected bool _canRealeaseItem;
    
    protected Food _foodData;
    
    // 비공개 필드
    // ============================================================


    public Transform TargetTransform => _targetTransform;
    
    public bool CanHold => _canHoldItem;
    public bool CanRelease => _canRealeaseItem;
    
    public Food FoodData => _foodData;

    public bool IsHolding => _foodData != null;
    public bool IsCooking => _cookProgress is CookwareJobEnum.Idle or CookwareJobEnum.Done;
    
    // 프로퍼티
    // ============================================================

    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);
    
    public abstract bool CanWork();
    public abstract bool CanWork(IHoldable holdable);
    
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
        else if (other.gameObject.layer == _foodLayer 
                 && !IsHolding)
        {
            SetFood(other.GetComponent<Food>());
            HoldItemPosition(TargetTransform);
        }
    }

    protected void ColliderExitCheck(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            
        }
        else if (other.gameObject.layer == _foodLayer 
                 && other.gameObject.transform == _foodData.FoodTransform)
        {
            UnHoldItemPosition();
            UnSetFood();
        }
    }
    
    // protected  메서드
    // ============================================================

    private void SetFood(Food food)
    {
        _foodData = food;
    }

    private void UnSetFood()
    {
        _foodData = null;
    }

    

    /// <summary>
    /// 요리중일때 음식을 못집어 올리게 bool값을 설정하는 함수
    /// </summary>
    private void CheckCookStatus()
    {
        if (!IsHolding) _cookProgress = CookwareJobEnum.Idle;
        else FoodData.CanHolding = !IsCooking;
    }
    
    // private 메서드
    // ============================================================
}
