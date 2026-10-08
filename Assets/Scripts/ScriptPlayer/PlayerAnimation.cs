using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PLAYER_ID _playerID;
    private Animator _animator;
    private InputManager _inputManager;

    private void Awake()
    {
        CacheComponents();
    }

    private void CacheComponents()  
    {  
        _animator = GetComponent<Animator>();  
        _inputManager = InputManager.Instance;
    }

    private void OnEnable() => SetDelegate();
    private void OnDisable() => UnSetDelegate();



    private void SetDelegate()
    {
        _inputManager.OnCookP1 += TryChopping;
        _inputManager.OnStopCookP1 += StopChopping;
        _inputManager.OnCookP2 += TryChopping;
        _inputManager.OnStopCookP2 += StopChopping;
    }
    
    private void UnSetDelegate()
    {
        _inputManager.OnCookP1 -= TryChopping;
        _inputManager.OnStopCookP1 -= StopChopping;
        _inputManager.OnCookP2 -= TryChopping;
        _inputManager.OnStopCookP2 -= StopChopping;
    }

    private void TryChopping()
    {
        
    }

    private void StopChopping()
    {
       
    }
    
}
