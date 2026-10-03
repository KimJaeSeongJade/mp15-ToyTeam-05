using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IHolder
{
    /// <summary>
    /// 물체가 위치할 트랜스폼
    /// </summary>
    [field: SerializeField] public Transform TargetTransform { get; private set; }
    
    /// <summary>
    /// 물건을 잡고 있는지 여부
    /// </summary>
    public bool IsHolding { get; private set; }
    
    /// <summary>
    /// 물건을 잡을 수 있는지 여부
    /// </summary>
    public bool CanHold { get; private set; }
    
    /// <summary>
    /// 물건을 놓을 수 있는지 여부
    /// </summary>
    public bool CanRelease { get; private set; }

    [SerializeField] private List<Food> _holdables;
    [SerializeField] private Food _currentHoldable;
    
    private PlayerMovement _playerMovement;
    
    private void Awake() => Init();
    
    private void Start()
    {
        BindHoldInputEvents();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>() == null) return;
        
        _holdables.Add(other.GetComponent<Food>());
    }
    
    private void OnTriggerExit(Collider other)
    {
        // if (other.GetComponent<Food>() == null) return;
        
        _holdables.Remove(other.GetComponent<Food>());
    }

    private void OnDisable()
    {
        UnBindHoldInputEvents();
    }
    
    // 들어올리기 / 내려놓기 함수
    private void PutItDown()
    {
        // if (_holdables.Count <= 0) return;
        
        if (IsHolding)
        {
            tempRelease();
        }
        else
        {
            tempHold();
        }
    }

    private void tempRelease()
    {
        _currentHoldable.Release();
        _currentHoldable.IsHolding = false;
        _currentHoldable.CanHolding = true;
        IsHolding = false;
        _currentHoldable = null;
    }
    
    private void tempHold()
    {
        if (_holdables.Count <= 0) return;
        foreach (var holdable in _holdables)
        {
            if (holdable.CanHolding)
            {
                holdable.Hold(this);
                _currentHoldable = holdable;
                holdable.IsHolding = true;
                holdable.CanHolding = false;
                IsHolding = true;
                return;
            }
        }
    }

    private void BindHoldInputEvents()
    {
        switch (_playerMovement._playerID)
        {
            case PLAYER_ID.PLAYER_1P:
                InputManager.Instance.OnIntaractP1 += PutItDown;
                break;
            case PLAYER_ID.PLAYER_2P:
                InputManager.Instance.OnIntaractP2 += PutItDown;
                break;
        }
    }

    private void UnBindHoldInputEvents()
    {
        InputManager.Instance.OnIntaractP1 -= PutItDown;
        InputManager.Instance.OnIntaractP2 -= PutItDown;
    }

    private void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }
}
