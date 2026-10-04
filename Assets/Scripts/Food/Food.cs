using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IFood, IHoldable
{
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
        if (playerID != PLAYER_ID.NONE) LastHolder = holder;
        _lastHoldingPlayerID = playerID;
        HoldItemPosition(holder);
    }

    public void Release()
    {
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
        
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        IsHolding = true;
    }

    /// <summary>
    /// 위치 고정을 해제하고 IsHolding을 false로 바꿈
    /// </summary>
    private void UnHoldItemPosition()
    {
        transform.SetParent(null);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.None;
        IsHolding = false;
    }

}
