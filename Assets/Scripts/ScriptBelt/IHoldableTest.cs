using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHoldableTest : MonoBehaviour, IHoldable
{
    public bool CanHolding { get; set; }
    public bool IsHolding { get; }
    public Rigidbody FoodRigidbody { get; }
    public Transform FoodTransform { get; }
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    
    public void Hold(IHolder holder)
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public Food FoodData { get; }
}
