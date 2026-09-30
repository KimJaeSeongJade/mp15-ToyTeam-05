using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFood 
{
    /// <summary>
    /// 음식 고유 ID
    /// </summary>
    public string FoodId { get; }

    /// <summary>
    /// 음식 이름
    /// </summary>
    public string FoodName { get; }
}
