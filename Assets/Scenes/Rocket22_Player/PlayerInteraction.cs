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

    [SerializeField] private IHoldable _holdable;
    // [SerializeField] private List<IHoldable> _holdable;

    private void OnTriggerEnter(Collider other)
    {
        if (!other is IHoldable) return;
        
        _holdable = other.GetComponent<IHoldable>();
        BindHoldEvents();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other is IHoldable) return;
    }

    private void PutItDown()
    {
        if (_holdable == null) return;

        if (IsHolding && !CanHold)
        {
            _holdable.Release();
            IsHolding = false;
            CanHold = true;
        }
        else
        {
            _holdable.Hold(this);
            IsHolding = true;
            CanHold = false;
        }
    }

    private void BindHoldEvents()
    {
        InputManager.Instance.OnIntaractP1 += PutItDown;
    }

    private void UnBindHoldEvents()
    {
        InputManager.Instance.OnIntaractP1 -= PutItDown;
    }
}
