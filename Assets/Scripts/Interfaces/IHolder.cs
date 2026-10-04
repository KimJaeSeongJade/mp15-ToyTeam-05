using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHolder
{
    /// <summary>
    /// 물체가 위치할 트랜스폼
    /// </summary>
    Transform TargetTransform { get; }
    
    /// <summary>
    /// 물건을 잡고 있는지 여부
    /// </summary>
    public bool IsHolding { get; }
    
    /// <summary>
    /// 물건을 잡을 수 있는지 여부
    /// </summary>
    public bool CanHold { get; }
    
    /// <summary>
    /// 물건을 놓을 수 있는지 여부
    /// </summary>
    public bool CanRelease { get; }
    
    /// <summary>
    /// 플레이어가 가지고 있던 음식 데이터와 참조를 비우는 메서드
    /// </summary>
    public void RemoveData();
    
}
