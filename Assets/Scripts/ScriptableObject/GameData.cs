using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")] 
public class GameData : ScriptableObject
{
    public const float START_GAMETIME = 180f;
    
    public int Player1Score;
    public int Player2Score;
    public float GameTimeLeft;

}
