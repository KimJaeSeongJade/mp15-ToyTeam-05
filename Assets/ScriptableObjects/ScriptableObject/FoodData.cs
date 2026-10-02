using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/FoodData")] 
public class FoodData : ScriptableObject
{
    public string FoodName;
    public int FoodPoint;
    public FoodEnum FoodId;
}
