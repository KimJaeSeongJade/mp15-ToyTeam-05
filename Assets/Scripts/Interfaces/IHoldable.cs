using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    /// <summary>
    /// 이 물체를 들고있는지 아닌지 상태를 반환
    /// </summary>
    public bool CanHolding { get; }
    
    /// <summary>
    /// 이 물체의 리자드바디
    /// </summary>
    public Rigidbody Rigidbody { get; }

    /// <summary>
    /// 물체에게서 문자열을 받아오는 메서드
    /// </summary>
    /// <returns></returns>
    public string GetData();
    
    /// <summary>
    /// 이 물체 잡기
    /// </summary>
    /// <param name="holder">IHoldable를 잡아둘 물체</param>
    public void Hold(IHolder holder);
    
    /// <summary>
    /// 이 물체 놓기
    /// </summary>
    public void Release();
}
