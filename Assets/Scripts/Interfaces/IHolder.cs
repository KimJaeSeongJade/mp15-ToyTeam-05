using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHolder
{
    public bool CanHold { get; }
    public bool CanRelease { get; }
    
}
