using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractor
{
    public bool CanInteract { get; }
    private bool isPlayer;  // true = 플레이어1 , false = 플레이어2

    
    private void Awake()
    {
       
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isPlayer)
            {
                
            }
            else
            {
                
            }

        }
    }
    
    
}
