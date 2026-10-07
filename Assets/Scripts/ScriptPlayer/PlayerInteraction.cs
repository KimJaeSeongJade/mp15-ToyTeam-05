using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    // [SerializeField] private Cookware _cookware;

    [SerializeField] public IInteractable _cookware { get; private set; }

    [SerializeField] private LayerMask _deskLayerMask;
    [SerializeField] private bool _IsDesk;

    private PlayerMovement _playerMovement;
    private Rigidbody _foodRigidbody;

    private float _checkDistance = 1.5f;

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

    // private void Update()
    // {
    //     // 레이저 그리는 함수 (테스트용)
    //     // DrawCheckRay();
    // }

    private void OnDisable()
    {
        UnBindHoldInputEvents();
    }
    
    // 레이저 그리는 함수 (테스트용)
    // private void DrawCheckRay()
    // {
    //     Vector3 pos = new Vector3(0, 1f, 0);
    //     Debug.DrawRay(transform.position + pos, transform.forward * _checkDistance, Color.red);
    // }
    //
    // private void DeskCheckRay()
    // {
    //     Vector3 pos = new Vector3(0, 1f, 0);
    //     Ray ray = new Ray((transform.position + pos), transform.forward);
    //
    //     if (Physics.Raycast(ray, out RaycastHit hit, _checkDistance, _deskLayerMask))
    //     {
    //         _IsDesk = true;
    //         _cookware = hit.collider.GetComponent<IInteractable>();
    //     }
    //     else
    //     {
    //         _IsDesk = false;
    //         _cookware = null;
    //     }
    // }

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
                IsHolding = true;
                return;
            }
        }
    }

    private void ThrowHoldItem(Food currentHoldItem, float forcePower)
    {
        if (currentHoldItem == null) return;

        _foodRigidbody = currentHoldItem.GetComponent<Rigidbody>();
        _foodRigidbody.AddForce(transform.forward * forcePower);
    }

    private void CookInteraction()
    {
        if (CanCook())
        {
            IsPressed = true;
            Debug.Log($"{IsPressed} : Cook");
        }
        else
        {
            IsPressed = false;
            Debug.Log($"{IsPressed} : UnCook");
        }
    }

    private void UnCookInteraction()
    {
        IsPressed = false;
        Debug.Log($"{IsPressed} : UnCook");
    }

    private bool CanCook()
    {
        if (_cookware != null)
        {
            if (IsHolding)
            {
                _cookware.Interact(this, _currentHoldable);
            }
            else
            {
                _cookware.Interact(this);
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
        if (other.TryGetComponent(out Food food))
        {
            _holdables.Add(food);
            food.OnReturnPool += RemoveData;
        }

        if (other.GetComponent<IInteractable>() != null)
        {
            _cookware = other.GetComponent<IInteractable>();
        }
    }

    private void CheckExitTrigger(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            RemoveData(food);
        }

        if (other.GetComponent<IInteractable>() != null)
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
            food.OnReturnPool -= RemoveData;
        }
    }
}