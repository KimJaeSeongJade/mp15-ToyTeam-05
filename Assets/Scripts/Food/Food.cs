using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IFood, IHoldable
{
    [SerializeField] private string _foodName;
    [SerializeField] private string _foodId;
    [SerializeField] internal int _foodPoint;

    private Rigidbody _rigidbody;

    public string FoodId => _foodId;
    public string FoodName => _foodName;
    
    public bool CanHolding { get; set; }
    public bool IsHolding { get; set; }

    public Food FoodData => this;

    public Rigidbody FoodRigidbody => _rigidbody;
    public Transform FoodTransform => transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        CanHolding = true;
    }

    public void Hold(IHolder holder)
    {
        HoldItemPosition(holder);
    }

    public void Release()
    {
        UnHoldItemPosition();
    }
    
    private void HoldItemPosition(IHolder holder)
    {
        transform.SetParent(holder.TargetTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        IsHolding = true;
    }

    private void UnHoldItemPosition()
    {
        transform.SetParent(null);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.None;
        IsHolding = false;
    }

}
