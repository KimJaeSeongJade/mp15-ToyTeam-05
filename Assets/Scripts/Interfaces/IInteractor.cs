using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractor
{
    /// <summary>
    /// 상호작용 가능 여부
    /// </summary>
    public bool CanInteract { get; }
    
    /// <summary>
    ///  상호작용 버튼이 눌린상태인지 체크하는 프로퍼티
    /// </summary>
    public bool IsPressed { get; }
    
    /// <summary>
    /// 플레이어 여부 / 번호를 체크하는 프로퍼티
    /// </summary>
    public PLAYER_ID PlayerCheck { get; }
}
