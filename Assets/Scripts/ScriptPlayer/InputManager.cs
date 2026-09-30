using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
   private Vector3 _moveDirectionP1;
   private bool _intaractP1;  // P1 잡기 놓기
   private bool _cookP1;  // P1 요리
   
   private Vector3 _moveDirectionP2;
   private bool _intaractP2;  // P2 잡기 놓기
   private bool _cookP2; // P2 요리
   //------------------------------------------------------

   // 플레이어 이동 관련 이벤트
   public event Action<Vector3> OnInputP1;
   public event Action<Vector3> OnInputP2;
   
   // 플레이어 상호작용 관련 이벤트
   public event Action OnIntaractP1;
   public event Action OnCookP1; 
   public event Action OnIntaractP2;
   public event Action OnCookP2;
   //-------------------------------------------------------
   
   private void Awake()
   {
      SetSingleton();
   }
   
   private void Update()
   {
      ReadInputs();
      
      // 이동 입력 받으면 해당 이벤트 구독중인 함수에 입력받은 백터값 넘김.
      OnInputP1?.Invoke(_moveDirectionP1);
      OnInputP2?.Invoke(_moveDirectionP2);

      // 해당 키 입력시 해당 이벤트 구독중인 함수 실행.
      if (_intaractP1) OnIntaractP1?.Invoke();
      if (_cookP1)  OnCookP1?.Invoke();

      if (_intaractP2) OnIntaractP2?.Invoke();
      if (_cookP2)  OnCookP2?.Invoke();
   }


   private void ReadInputs()
   {
       float _HorizP1 = Input.GetAxisRaw("HorizontalP1");
       float _VertP1 = Input.GetAxisRaw("VerticalP1");
   
       float _HorizP2 = Input.GetAxisRaw("HorizontalP2");
       float _VertP2 = Input.GetAxisRaw("VerticalP2");
      
      // 입력받은 이동키 변수에 담기
      _moveDirectionP1 = new Vector3(_HorizP1, 0, _VertP1);
      _moveDirectionP2 = new Vector3(_HorizP2, 0, _VertP2);
      
      _intaractP1 = Input.GetKeyDown(KeyCode.G);  // 잡기 놓기
      _cookP1 = Input.GetKey(KeyCode.H);  // 조리 (지속 입력) 
      
      _intaractP2 = Input.GetKeyDown(KeyCode.Keypad0); // 잡기 놓기
      _cookP2 = Input.GetKey(KeyCode.Keypad1);  // 조리 (지속 입력)
      
   }
}
