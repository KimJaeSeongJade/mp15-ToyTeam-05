using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// 상호작용
    /// </summary>
    /// <param name="interactor">상호작용 주체</param>
    public void Interact(IInteractor interactor);
    
    /// <summary>
    /// 상호작용
    /// </summary>
    /// <param name="interactor">상호작용 주체</param>
    /// <param name="Hold">상호작용에 사용될 대상</param>
    public void Interact(IInteractor interactor, IHoldable holdable);
    
    public void RemoveData(IInteractor interactor);

    public Transform TransformInteract { get; }
}
