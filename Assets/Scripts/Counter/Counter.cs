using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    private bool isPlayer;  // true = 플레이어1 , false = 플레이어2
    
   
    public void Interact(IInteractor interactor)
    {
        
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
    }
    
    private bool IsOrder(Food playerFood) //주문서 == 요리 판별
    {
        
        
        return false;
    }
}
