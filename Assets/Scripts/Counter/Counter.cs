using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public RemoveOrderUI _removeOrderUI;
    public GameData _gameData;
    public void Interact(IInteractor interactor)
    {
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        Food food = holdable.FoodData;

        int index = -1;
        string foodId = holdable.FoodData.FoodId;
       
        for (int i = 0; i < OrderManager.Instance.OrderList.Count; i++)
        {
            if (OrderManager.Instance.OrderList[i].FoodId == foodId)
            {
                index = i;
            }
        }
        
        if(index != -1)
        {
            OrderManager.Instance._successIndex = index;
            _removeOrderUI.RemoveOrderUi();
            OrderManager.Instance.OrderList.RemoveAt(index);  // 리스트에서 삭제
            
            if (interactor.PlayerCheck == PLAYER_ID.PLAYER_1P)
            {
                _gameData.Player1Score += holdable.FoodData._foodPoint;
                _gameData.Player1Food++;
            }
            else if (interactor.PlayerCheck == PLAYER_ID.PLAYER_2P)
            {
                _gameData.Player2Score += holdable.FoodData._foodPoint;
                _gameData.Player2Food++;
            }
            holdable.FoodData.ReturnToPool();
        }
    }
}
