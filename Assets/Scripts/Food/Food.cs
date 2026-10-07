using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IFood, IHoldable
{
    public event System.Action<Food> OnReturnPool;
    public event System.Action<Food> OnPlayerHold;
    
    [SerializeField] private string _foodName;
    [SerializeField] private string _foodId;
    [SerializeField] internal int _foodPoint;

    private Rigidbody _rigidbody;
    private PLAYER_ID _lastHoldingPlayerID = PLAYER_ID.NONE;

    public string FoodId => _foodId;
    public string FoodName => _foodName;
    
    public bool CanHolding { get; set; }
    public bool IsHolding { get; set; }
    public IHolder LastHolder { get; set; }

    public Food FoodData => this;

    public PLAYER_ID PlayerID => _lastHoldingPlayerID;
    public Rigidbody FoodRigidbody => _rigidbody;
    public Transform FoodTransform => transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        CanHolding = true;
    }

    public void Hold(IHolder holder, PLAYER_ID playerID)
    {
        if (playerID != PLAYER_ID.NONE)
        {
            LastHolder = holder;
            CanHolding = false;
            OnPlayerHold?.Invoke(this);
        }
        else
        {
            CanHolding = true;
        }
        HoldItemPosition(holder);
        _lastHoldingPlayerID = playerID;
    }

    public void Release()
    {
        UnHoldItemPosition();
    }

    /// <summary>
    /// 이 오브젝트 활성화
    /// </summary>
    public void ActiveThisFood()
    {
        this.gameObject.SetActive(true);
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void ActiveCookFood()
    {
        this.gameObject.SetActive(true);
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// 오브젝트 풀로 돌아감
    /// </summary>
    public void ReturnToPool()
    {
        this.gameObject.SetActive(false);
        // Debug.Log($"{this.name}이 비명을 지르며 오브젝트 풀 너머로 사라집니다...");
        OnReturnPool?.Invoke(this);
        UnHoldItemPosition();
    }
    
    /// <summary>
    /// 잡은 오브젝트의 위치에 고정시키고 IsHolding을 true로 변경
    /// </summary>
    /// <param name="holder"></param>
    private void HoldItemPosition(IHolder holder)
    {
        transform.SetParent(holder.TargetTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
        IsHolding = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// 위치 고정을 해제하고 IsHolding을 false로 바꿈
    /// </summary>
    private void UnHoldItemPosition()
    {
        transform.SetParent(null);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.None;
        CanHolding = true;
        IsHolding = false;
    }

}
