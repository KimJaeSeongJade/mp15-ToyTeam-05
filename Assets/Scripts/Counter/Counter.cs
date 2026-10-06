using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    public RemoveOrderUI _removeOrderUI;
    public GameData _gameData;
    
    [SerializeField] private SpecialOrder _specialOrder;
    public void Interact(IInteractor interactor)
    {
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        int index = OrderManager.Instance.OrderList.IndexOf(holdable.FoodData); // 오더리스트에 있나
        if (index == -1) return;
      
        else
        {
            OrderManager.Instance._successIndex = index;
            _removeOrderUI.RemoveOrderUi();
            
            OrderManager.Instance.OrderList.RemoveAt(index);  // 리스트에서 삭제
            
            if (interactor.PlayerCheck == PLAYER_ID.PLAYER_1P)
            {
                if (holdable.FoodData.FoodId == "020411")
                {
                    _specialOrder.RandomSpecial(1);
                }
                
                _gameData.Player1Score += holdable.FoodData._foodPoint;
                _gameData.Player1Food++;
            }
            else if (interactor.PlayerCheck == PLAYER_ID.PLAYER_2P)
            {
                if (holdable.FoodData.FoodId == "020411")
                {
                    _specialOrder.RandomSpecial(0);
                }
                
                _gameData.Player2Score += holdable.FoodData._foodPoint;
                _gameData.Player2Food++;
            }
            
            Destroy(holdable.FoodData);
        }
    }
}
