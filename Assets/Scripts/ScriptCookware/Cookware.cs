using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable, IHolder
{
    protected readonly int _playerLayerMask = 6;
    protected readonly int _itemLayerMask = 12;
    protected Transform _foodTransform;
    protected Rigidbody _foodRigidbody;
    [SerializeField] protected Transform _holdTransform;
    
    public Rigidbody FoodRigidbody => _foodRigidbody;
    public Transform FoodTransform => _foodTransform;
    public Transform TargetTransform => _holdTransform;
    
    public abstract bool IsHolding { get; }
    public abstract bool CanHolding { get; }
    public abstract bool CanHold { get; }
    public abstract bool CanRelease { get; }
    
    public abstract Food FoodData { get; }

    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);

    public abstract void Hold(IHolder holder);

    public abstract void Release();
    
    protected void HoldItem()
    {
        FoodTransform.rotation = Quaternion.Euler(Vector3.zero);
        FoodTransform.position = TargetTransform.position;
        FoodRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        FoodData.IsHolding = true;
    }
    protected void UnHoldItem()
    {
        FoodRigidbody.constraints = RigidbodyConstraints.None;
        FoodData.IsHolding = false;
    }
    
    // 메서드
    // ============================================================
}
