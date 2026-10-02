using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour,IInteractable
{
    [SerializeField] private int _DiscountScore;
    public GameData _gameData;
    public void Interact(IInteractor interactor)
    {
    }

    public void Interact(IInteractor interactor, IHoldable holdable)
    { 
        if(holdable == null) return;
        if (interactor.IsPlayer1)
        {
            _gameData.Player1Score -= _DiscountScore;
        }
        else if (!interactor.IsPlayer1)
        {
            _gameData.Player2Score -= _DiscountScore;
        }
        Destroy(holdable.FoodData);
    }
}
