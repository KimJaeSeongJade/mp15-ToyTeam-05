using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    /// <summary>
    /// 이 물체를 들 수 있는지 여부를 반환
    /// </summary>
    public bool CanHolding { get; }
    
    /// <summary>
    /// 이 물체가 잡혀있는 상태인지 반환
    /// </summary>
    public bool IsHolding { get; }

    public IHolder LastHolder { get; }
    public PLAYER_ID PlayerID { get; }
    public Rigidbody FoodRigidbody { get; }
    public Transform FoodTransform { get; }

    /// <summary>
    /// 물체에게서 문자열을 받아오는 메서드
    /// </summary>
    /// <returns></returns>
    public Food FoodData { get; }
    
    /// <summary>
    /// 이 물체 잡기
    /// </summary>
    /// <param name="holder">IHoldable를 잡아둘 물체</param>
    public void Hold(IHolder holder, PLAYER_ID ID);
    
    /// <summary>
    /// 이 물체 놓기
    /// </summary>
    public void Release();
}
