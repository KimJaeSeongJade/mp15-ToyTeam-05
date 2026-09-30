using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable
{
    public abstract Rigidbody Rigidbody { get; }
    
    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);
    
    /// <summary>
    /// 상호작용 주체와 '음식'을 전달받는 메서드
    /// </summary>
    /// <param name="holder"></param>
    // public abstract void Interact()

    public abstract void Hold(IHolder holder);

    public abstract void Release();
    
    // 메서드
    // ============================================================
}
