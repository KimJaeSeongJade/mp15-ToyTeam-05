using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable, IHolder
{
    public abstract Rigidbody Rigidbody { get; }
    public abstract Transform TargetTransform { get;}
    public abstract bool IsHolding { get; }
    public abstract bool CanHolding { get; }
    public abstract bool CanHold { get; }
    public abstract bool CanRelease { get; }

    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);

    public abstract string GetData();

    public abstract void Hold(IHolder holder);

    public abstract void Release();
    
    // 메서드
    // ============================================================
}
