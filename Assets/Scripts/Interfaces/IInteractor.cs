using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractor
{
    /// <summary>
    /// 상호작용 가능 여부
    /// </summary>
    public bool CanInteract { get; }
    
}
