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
    }

    public void Hold(IHolder holder)
    {
        // 나중에 손 위치나 들 위치 정해서 수정
        transform.SetParent(holder.TargetTransform);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        transform.SetParent(null);
    }

}
