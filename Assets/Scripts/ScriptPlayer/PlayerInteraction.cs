using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IHolder, IInteractor
{
    /// <summary>
    /// 물체가 위치할 트랜스폼
    /// </summary>
    [field: SerializeField]
    public Transform TargetTransform { get; private set; }

    /// <summary>
    /// 물건을 잡고 있는지 여부
    /// </summary>
    public bool IsHolding => _currentHoldable != null;

    /// <summary>
    /// 물건을 잡을 수 있는지 여부
    /// </summary>
    public bool CanHold { get; private set; }

    /// <summary>
    /// 물건을 놓을 수 있는지 여부
    /// </summary>
    public bool CanRelease { get; private set; }

    public PlayerInteraction Player => this;

    public PLAYER_ID PlayerCheck => _playerMovement._playerID;

    [SerializeField] private List<Food> _holdables;
    [SerializeField] private Food _currentHoldable;

    /// <summary>
    /// 상호작용 가능 여부
    /// </summary>
    public bool CanInteract => _canFindInteractable; // 상호작용 가능한지 여부 (다른 상호작용중이라던가 하면 X)

    public bool IsPressed { get; private set; } // 버튼 누르고 있는지 여부

    public bool IsPlayer1 { get; private set; }
    
    private PlayerMovement _playerMovement;
    private Rigidbody _foodRigidbody;
    
    [SerializeField] private GameObject _pointLightPrefab;
    private GameObject _pointLight;

    private Vector3 _trackerHeight = new Vector3(0, 0.2f, 0);

    private void Awake() => Init();

    private void OnEnable() => BindHoldInputEvents();

    private void OnTriggerEnter(Collider other)
    {
        CheckEnterTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckStayTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckExitTrigger(other);
    }

    private void Update()
    { 
        GetInteractable();
        CheckCanInteract();
    }

    private void OnDisable() => UnBindHoldInputEvents();
    
    private void OnEnableInteractPoint()
    {
        if (CurrentInteractable != null)
        {
            _pointLight.transform.position = CurrentInteractable.TransformInteract.position + _trackerHeight;
            _pointLight.gameObject.SetActive(true);
        }
    }

    private void OnDisableInteractPoint()
    {
        if (CurrentInteractable == null)
        {
            _pointLight.gameObject.SetActive(false);
        }
        else
        {
            OnEnableInteractPoint();
        }
    }

    // 들어올리기 / 내려놓기 함수
    private void PutItDown()
    {
        if (IsHolding)
        {
            ReleaseItem();
            if (_holdables.Count <=0 ) return;
            
            ThrowHoldItem(_holdables[0], 100f);
        }
        else
        {
            HoldItem();
        }
    }

    private void ReleaseItem()
    {
        _currentHoldable.Release();
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
            CanCookTrue();
        }
        else
        {
            CanCookFalse();
        }
    }

    private void UnCookInteraction()
    {
        IsPressed = false;
        // Debug.Log($"{IsPressed} : UnCook");
    }

    private bool CanCook()
    {
        return (CurrentInteractable != null);
    }

    private void CanCookTrue()
    {
        if (IsHolding)
        {
            CurrentInteractable.Interact(this, _currentHoldable);
        }
        else
        {
            CurrentInteractable.Interact(this);
        }
        // Debug.Log("Cookware");
        IsPressed = true;
        // Debug.Log($"{IsPressed} : Cook");
    }

    private void CanCookFalse()
    {
        if (IsHolding)
        {
            OnThrow?.Invoke();
            ReleaseItem();
            if (_holdables.Count <=0 ) return;
            ThrowHoldItem(_holdables[0], 1000f);
        }
        // Debug.Log("UnCookware");
        IsPressed = false;
        // Debug.Log($"{IsPressed} : UnCook");
    }

    private void CheckCanInteract()
    {
        if (isChopping)
        {
            _playerMovement.enabled = false;
        }
        else
        {
            _playerMovement.enabled = true;
        }
    }

    private void CheckEnterTrigger(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            _holdables.Add(food);
            food.OnReturnPool += RemoveData;
        }

        if (other.TryGetComponent(out IInteractable inter))
        {
            CheckEnterTrigger(inter);
            OnEnableInteractPoint();
        }
    }

    private void CheckStayTrigger(Collider other)
    {
        OnEnableInteractPoint();
    }

    private void CheckExitTrigger(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            RemoveData(food);
        }

        if (other.TryGetComponent(out IInteractable inter))
        {
            CheckoutTrigger(inter);
            OnDisableInteractPoint();
            if (_interactables == null)
            {
                CurrentInteractable = null;
                IsPressed = false;
                isChopping = false;
            }
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
        _pointLight = Instantiate(_pointLightPrefab);
        _pointLight.gameObject.SetActive(false);
    }

    /// <summary>
    /// 별개 메서드로 구현 필요, I Holder 참조
    /// </summary>
    public void RemoveData(Food food)
    {
        if (food == _currentHoldable) _currentHoldable = null;
        if (_holdables.Contains(food))
        {
            _holdables.Remove(food);
            food.OnReturnPool -= RemoveData;
        }
    }
    
    // =============================================================

    public event System.Action OnThrow;
    private bool _isInterListEmpty => _interactables.Count <= 0;
    private bool _canFindInteractable => CurrentInteractable != null;
    public bool isChopping;

    private List<IInteractable> _interactables = new();
    public IInteractable CurrentInteractable;
    
    private void CheckEnterTrigger(IInteractable inter)
    {
        _interactables.Add(inter);
    }

    private void CheckoutTrigger(IInteractable inter)
    {
        if (_interactables.Contains(inter))
        {
            _interactables.Remove(inter);
            if (inter ==  CurrentInteractable)
            {
                inter.RemoveData(this);
                isChopping = false;
                CurrentInteractable = null;
            }
        }
    }

    private void GetInteractable()
    {
        if (_canFindInteractable || _isInterListEmpty) return;
        CurrentInteractable = _interactables[0];
    }
}