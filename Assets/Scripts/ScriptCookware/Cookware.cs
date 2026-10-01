using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable, IHolder
{
    public abstract Rigidbody FoodRigidbody { get; }
    public abstract Transform FoodTransform { get; }
    public abstract Transform TargetTransform { get;}
    public abstract bool IsHolding { get; }
    public abstract bool CanHolding { get; }
    public abstract bool CanHold { get; }
    public abstract bool CanRelease { get; }
    
    public abstract Food FoodData { get; }

    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);

    public abstract void Hold(IHolder holder);

    public abstract void Release();
    
    // 메서드
    // ============================================================
}
