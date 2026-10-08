using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInteraction _player;
    private PLAYER_ID _playerID;
    private Animator _animator;

    private bool _isMoving;
    private bool _isHolding;
    private bool _isChopping;

    private void Awake()
    {
        CacheComponents();
    }

    private void CacheComponents()  
    {  
        _player = GetComponent<PlayerInteraction>();
        _animator = GetComponent<Animator>();  
    }

    private void IsPlayerMoving()
    {
        
    }

    private void HandsIdle()
    {
        
    }
    

    private void StartChopping()
    {
        
    }

    private void StopChopping()
    {
       
    }
    
}
