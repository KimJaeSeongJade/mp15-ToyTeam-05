using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IFood, IHoldable
{
    [SerializeField] private string _foodName;
    [SerializeField] private string _foodId;
    
    private Rigidbody _rigidbody;
    
    public string FoodId => _foodId;
    public string FoodName => _foodName;
    
    public bool CanHolding { get; set; }

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody =  GetComponent<Rigidbody>();
    }

    public void Hold(IHolder holder)
    {
        // 나중에 손 위치나 들 위치 정해서 수정
        transform.SetParent(holder.TargetTransform);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public string GetData()
    {
        return _foodId;
    }

    public void Release()
    {
        transform.SetParent(null);
    }

}
