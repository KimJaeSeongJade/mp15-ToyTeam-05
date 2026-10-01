using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    [SerializeField] private Food _resultFood;      // 조합 음식
    [SerializeField] private float _cookTime;       // 조리 시간
    [SerializeField] private int _score;            // 점수

    public Food ResultFood => _resultFood;
    public float CookTime => _cookTime;
    public int Score => _score;
}
