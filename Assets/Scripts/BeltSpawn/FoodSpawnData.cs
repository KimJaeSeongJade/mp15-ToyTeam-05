using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodSpawnData
{
    [SerializeField] private Food _food;
    [SerializeField] private float _cooldown;

    public Food Food => _food;
    public float Cooldown => _cooldown;
}
