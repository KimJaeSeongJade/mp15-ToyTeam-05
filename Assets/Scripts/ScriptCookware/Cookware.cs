using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cookware : MonoBehaviour, IInteractable, IHoldable
{
    private readonly int _playerLayerMask = (1 << 6);
    
    // 상수 / Readonly 필드
    // ============================================================
    
    private IInteractor _interactor;
    
    // 인스턴스 필드
    // ============================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayerMask)
        {
            _interactor = other.GetComponent<IInteractor>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayerMask)
        {
            _interactor = null;
        }
    }
    
    // 이벤트
    // ============================================================
    
    public abstract void Interact(IInteractor interactor);

    public abstract void Interact(IInteractor interactor, IHoldable holdable);

    public abstract void Hold(IHolder holder);

    public abstract void Release();
    
    // 메서드
    // ============================================================
}
