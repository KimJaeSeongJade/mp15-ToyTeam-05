using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    
    
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
