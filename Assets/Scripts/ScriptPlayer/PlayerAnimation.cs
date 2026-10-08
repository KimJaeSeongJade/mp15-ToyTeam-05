using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private readonly int _isChoppingID = Animator.StringToHash(ChopString);
    private readonly int _isHoldingID = Animator.StringToHash(HoldString);
    private readonly int _isMovingID = Animator.StringToHash(MoveString);
    private readonly int _throwID = Animator.StringToHash(ThrowString);
    
    private PlayerInteraction _playerInteract;
    private PlayerMovement _playerMove;
    private PLAYER_ID _playerID;
    private Animator _animator;

    private const string ChopString = "IsChopping";
    private const string HoldString = "IsHolding";
    private const string MoveString = "IsMoving";
    private const string ThrowString = "Throw";

    private bool _isMoving => _playerMove.IsMoving;
    private bool _isHolding => _playerInteract.IsHolding;
    private bool _isChopping => _playerInteract.isChopping;

    private void Awake() => CacheComponents();
    
    private void Update() => UpdateStatus();

    private void OnEnable() => _playerInteract.OnThrow += OnThrow;
    private void OnDisable() => _playerInteract.OnThrow -= OnThrow;

    private void CacheComponents()  
    {  
        _playerMove = GetComponent<PlayerMovement>();
        _playerInteract = GetComponent<PlayerInteraction>();
        _animator = GetComponent<Animator>();  
    }

    private void UpdateStatus()
    {
        _animator.SetBool(_isChoppingID, _isChopping);
        _animator.SetBool(_isHoldingID, _isHolding);
        _animator.SetBool(_isMovingID, _isMoving);
    }
    
    private void OnThrow()
    {
        _animator.SetTrigger(_throwID);
    }
    
}
