using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
   private float _HorizP1 = Input.GetAxisRaw("HorizontalP1");
   private float _VertP1 = Input.GetAxisRaw("VerticalP1");
   private bool _intaractP1 = Input.GetKeyDown(KeyCode.G);  // 잡기 놓기
   private bool _skillP1 = Input.GetKeyDown(KeyCode.H);  // 조리 
   
   private float _HorizP2 = Input.GetAxisRaw("HorizontalP2");
   private float _VertP2 = Input.GetAxisRaw("VerticalP2");
   private bool _intaractP2 = Input.GetKeyDown(KeyCode.Keypad0); // 잡기 놓기
   private bool _skillP2 = Input.GetKeyDown(KeyCode.Keypad1);  // 조리

   private Vector3 _moveDirectionP1; 
   private Vector3 _moveDirectionP2;
   
   
   private void Awake()
   {
      SetSingleton();
   }

   private void Start()
   {
   }

   private void Update() => ReadInputs();
   
   
   private void ReadInputs()
   {
      _moveDirectionP1 = new Vector3(_HorizP1, 0, _VertP1);
      _moveDirectionP2 = new Vector3(_HorizP2, 0, _VertP2);
      
   }
   
   
   



}
