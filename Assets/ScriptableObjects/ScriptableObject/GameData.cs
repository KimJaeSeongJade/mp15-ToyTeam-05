using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")] 
public class GameData : ScriptableObject
{
    public const float START_GAMETIME = 300f;
    
    public int Player1Score;
    public int Player2Score;

    // 제출 음식 수
    public int Player1Food;
    public int Player2Food;

    public float GameTimeLeft;

}
