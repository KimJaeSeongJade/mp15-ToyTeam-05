using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHoldableTest : MonoBehaviour, IHoldable
{
    public bool CanHolding { get; set; }
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    
    public void Hold(IHolder holder)
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public string GetData()
    {
        return "";
    }
}
