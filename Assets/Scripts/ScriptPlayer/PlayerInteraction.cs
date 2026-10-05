using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IHolder, IInteractor
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

    public PLAYER_ID PlayerCheck => _playerMovement._playerID;

    [SerializeField] private List<Food> _holdables;
    [SerializeField] private Food _currentHoldable;
    
    /// <summary>
    /// 상호작용 가능 여부
    /// </summary>
    public bool CanInteract { get; private set; } // 상호작용 가능한지 여부 (다른 상호작용중이라던가 하면 X)
    
    public bool IsPressed { get; private set; } // 버튼 누르고 있는지 여부
    
    public bool IsPlayer1 { get; private set; }
    
    [SerializeField] private Cookware _cookware;
    [SerializeField] private bool _CanCookware;
    
    private PlayerMovement _playerMovement;
    private Rigidbody _foodRigidbody;
    
    private void Awake() => Init();
    
    private void OnEnable()
    {
        BindHoldInputEvents();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckEnterTrigger(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        CheckExitTrigger(other);
    }

    private void OnDisable()
    {
        UnBindHoldInputEvents();
    }
    
    // 들어올리기 / 내려놓기 함수
    private void PutItDown()
    {
        if (IsHolding)
        {
            ThrowHoldItem(_currentHoldable, 100f);
            ReleaseItem();
        }
        else
        {
            HoldItem();
        }
    }

    private void ReleaseItem()
    {
        _currentHoldable.Release();
        // _currentHoldable.IsHolding = false;
        _currentHoldable.CanHolding = true;
        IsHolding = false;
        
        _currentHoldable = null;
    }
    
    private void HoldItem()
    {
        if (_holdables.Count <= 0) return;
        foreach (var holdable in _holdables)
        {
            if (holdable.CanHolding)
            {
                holdable.Hold(this, _playerMovement._playerID);
                _currentHoldable = holdable;
                // holdable.IsHolding = true;
                holdable.CanHolding = false;
                IsHolding = true;

                return;
            }
        }
    }

    private void ThrowHoldItem(Food currentHoldItem, float forcePower)
    {
        _foodRigidbody = currentHoldItem.GetComponent<Rigidbody>();
        _foodRigidbody.AddForce(transform.forward * forcePower);
    }

    private void CookInteraction()
    {
        if (CanCook())
        {
            IsPressed = true;
            CanInteract = false;
            Debug.Log($"{IsPressed} : Cook");
        }
        else
        {
            IsPressed = false;
            CanInteract = true;
            Debug.Log($"{IsPressed} : UnCook");
        }
    }

    private void UnCookInteraction()
    {
        IsPressed = false;
        CanInteract = true;
        Debug.Log($"{IsPressed} : UnCook");
    }

    private bool CanCook()
    {
        if (_cookware)
        {
            if (!IsHolding)
            {
                _cookware.Interact(this);
            }
            else
            {
                ThrowHoldItem(_currentHoldable, 1000f);
                ReleaseItem();
            }
            Debug.Log("Cookware");
            return true;
        }
        else
        {
            ThrowHoldItem(_currentHoldable, 1000f);
            ReleaseItem();
            Debug.Log("UnCookware");
            return false;
        }
    }

    private void CheckEnterTrigger(Collider other)
    {
        if (other.GetComponent<Food>() != null) _holdables.Add(other.GetComponent<Food>());
        
        if (other.GetComponent<Cookware>() != null) _cookware = other.GetComponent<Cookware>();
    }
    
    private void CheckExitTrigger(Collider other)
    {
        _holdables.Remove(other.GetComponent<Food>());
        
        if (other.GetComponent<Cookware>())
        {
            _cookware = null;
            IsPressed = false;
        }
    }
    
    private void BindHoldInputEvents()
    {
        switch (_playerMovement._playerID)
        {
            case PLAYER_ID.PLAYER_1P:
                InputManager.Instance.OnIntaractP1 += PutItDown;
                InputManager.Instance.OnCookP1 += CookInteraction;
                InputManager.Instance.OnStopCookP1 += UnCookInteraction;
                break;
            case PLAYER_ID.PLAYER_2P:
                InputManager.Instance.OnIntaractP2 += PutItDown;
                InputManager.Instance.OnCookP2 += CookInteraction;
                InputManager.Instance.OnStopCookP2 += UnCookInteraction;
                break;
        }
    }

    private void UnBindHoldInputEvents()
    {
        switch (_playerMovement._playerID)
        {
            case PLAYER_ID.PLAYER_1P:
                InputManager.Instance.OnIntaractP1 -= PutItDown;
                InputManager.Instance.OnCookP1 -= CookInteraction;
                InputManager.Instance.OnStopCookP1 -= UnCookInteraction;
                break;
            case PLAYER_ID.PLAYER_2P:
                InputManager.Instance.OnIntaractP2 -= PutItDown;
                InputManager.Instance.OnCookP2 -= CookInteraction;
                InputManager.Instance.OnStopCookP2 -= UnCookInteraction;
                break;
        }
    }

    private void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// 별개 메서드로 구현 필요, I Holder 참조
    /// </summary>
    public void RemoveData(Food food)
    {
        if (_holdables.Contains(food))
        {
            _holdables.Remove(food);
        }
    }
}
