using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public GameData _gameData;
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
                _gameData.Player1Score += holdable.FoodData._foodPoint;
                _gameData.Player1Food++;
            }
            else if (!interactor.IsPlayer1)
            {
                _gameData.Player2Score += holdable.FoodData._foodPoint;
                _gameData.Player2Food++;
            }
            
            Destroy(holdable.FoodData);
        }
    }
}
