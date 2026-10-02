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

    private void OnTriggerEnter(Collider other)
    {
        if (!other is IHoldable || _holdable != null) return;

        // _holdable이 비어있으면 IHoldable를 가지고 있는다
        if (_holdable == null)
        {
            _holdable = other.GetComponent<IHoldable>();
        }
        
        BindHoldInputEvents();
    }
    
    // 음식 하나 들고있는 상태에서 다른음식이 겹치면 이후에 들고있는 음식이 안내려놔짐
    private void OnTriggerExit(Collider other)
    {
        if (!other is IHoldable) return;
        
        // _holdable이 비어있지 않으면 _holdable를 비운다
        if (_holdable != null)
        {
            _holdable = null;
        }
        
        UnBindHoldInputEvents();
    }

    // Food 스크립트 변경으로 수정 필요할수 있음
    // 들어올리기 / 내려놓기 함수
    private void PutItDown()
    {
        if (_holdable == null) return;

        if (IsHolding && !CanHold)
        {
            // 내려놓기 동작 부분
            _holdable.Release();
            _holdable = null;
            IsHolding = false;
            CanHold = true;
        }
        else
        {
            // 들기 동작 부분
            _holdable.Hold(this);
            IsHolding = true;
            CanHold = false;
        }
    }

    private void BindHoldInputEvents()
    {
        InputManager.Instance.OnIntaractP1 += PutItDown;
    }

    private void UnBindHoldInputEvents()
    {
        InputManager.Instance.OnIntaractP1 -= PutItDown;
    }
}
