using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable, IHolder
{
    [SerializeField] protected Transform _targetTransform;
    
    protected int _playerLayer = 6;
    protected int _foodLayer = 12;

    protected bool _canHolding;
    
    protected bool _isHolding;
    protected bool _canHold;
    protected bool _canRealease;
    
    protected Rigidbody _foodRigidbody;
    protected Transform _foodTransform;

    protected Food _foodData;
    
    // 비공개 필드
    // ============================================================

    public bool CanHolding => _canHolding;
    public Transform TargetTransform => _targetTransform;

    public bool IsHolding => _isHolding;
    public bool CanHold => _canHold;
    public bool CanRelease => _canRealease;
    
    public Rigidbody FoodRigidbody => _foodRigidbody;
    public Transform FoodTransform => _foodTransform;
    
    public Food FoodData => _foodData;
    
    // 프로퍼티
    // ============================================================

    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);
    
    // 추상 메서드
    // ============================================================
    
    public void Hold(IHolder holder)
    {
        UnHoldItemPosition();
        HoldItemPosition(TargetTransform);
    }

    public abstract void Release();
    
    protected void ColliderEnterCheck(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            
        }
        else if (other.gameObject.layer == _foodLayer)
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
        else if (other.gameObject.layer == _foodLayer)
        {
            UnHoldItemPosition();
            UnSetFood();
        }
    }
    
    protected void SetFood(Food food)
    {
        _foodData = food;
        _foodTransform = _foodData.FoodTransform;
        _foodRigidbody = _foodData.FoodRigidbody;
    }

    protected void UnSetFood()
    {
        _foodData = null;
        _foodTransform = null;
        _foodRigidbody = null;
    }
    
    protected void HoldItemPosition(Transform targetTransform)
    {
        _foodTransform.rotation = Quaternion.Euler(Vector3.zero);
        _foodTransform.position = targetTransform.position;
        _foodRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _foodData.IsHolding = true;
    }
    protected void UnHoldItemPosition()
    {
        _foodRigidbody.velocity = Vector3.zero;
        _foodRigidbody.constraints = RigidbodyConstraints.None;
        _foodData.IsHolding = false;
    }


}
