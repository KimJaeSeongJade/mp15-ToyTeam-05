using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimation", menuName = "ScriptableObjects/PlayerAnimation")] 
public class AnimationData : ScriptableObject
{
    public bool isArmFront;
    public bool isChopping;
}
