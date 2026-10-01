using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractor
{
    /// <summary>
    /// 상호작용 가능 여부
    /// </summary>

    public bool CanInteract { get; } // 상호작용 가능한지 여부 (다른 상호작용중이라던가 하면 X)
    
    public bool IsPressed { get; } // 버튼 누르고 있는지 여부
    
    public bool IsPlayer1 { get; }

}
