using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public void Interact(IInteractor interactor)
    {
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        int index = OrderManager.Instance.OrderList.IndexOf(holdable.FoodData); // 오더리스트에 있나
        if (index == -1) return;
        else
        {
            OrderManager.Instance.OrderList.RemoveAt(index);  // 리스트에서 삭제
            
            if (interactor.IsPlayer1)
            {
                // P1 점수 주기
                // P1 누적 완성음식 갯수++
            }
            else if (!interactor.IsPlayer1)
            {
                // P2 점수 주기
                // P2 누적 완성음식 갯수++
            }
            
            Destroy(holdable.FoodData);
        }
    }
}
