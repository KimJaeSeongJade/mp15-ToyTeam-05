using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
   private float _HorizP1;
   private float _VertP1;
   public Vector3 _moveDirectionP1;
   private bool _holdP1;  // P1 상호작용1 잡기 놓기
   private bool _intaractDownP1;  // P2 상호작용2 조리
   private bool _intaractUpP1;
   
   private float _HorizP2;
   private float _VertP2;
   public Vector3 _moveDirectionP2;
   private bool _holdP2;  // P2 상호작용1 잡기 놓기
   private bool _intaractDownP2; // P2 상호작용2 조리
   private bool _intaractUpP2;
   private bool _pauseBreak;

   public int num = 0;
   //------------------------------------------------------
   
   // 플레이어 이동 관련 이벤트
   public event Action<Vector3> OnInputP1;
   public event Action<Vector3> OnInputP2;
   
   // 플레이어 상호작용 관련 이벤트
   public event Action OnIntaractP1;
   public event Action OnCookP1; 
   public event Action OnStopCookP1; 
   public event Action OnIntaractP2;
   public event Action OnCookP2;
   public event Action OnStopCookP2;
   
   // 일시정지 이벤트
   public event Action OnPauseBreak;
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
      if (_pauseBreak) OnPauseBreak?.Invoke();
      if (_holdP1) OnIntaractP1?.Invoke();
      if (_intaractDownP1)  OnCookP1?.Invoke();
      if (_intaractUpP1) OnStopCookP1?.Invoke();

      if (_holdP2) OnIntaractP2?.Invoke();
      if (_intaractDownP2) OnCookP2?.Invoke();
      if (_intaractUpP2) OnStopCookP2?.Invoke();
   }


   private void ReadInputs()
   {
       _pauseBreak = Input.GetKeyDown(KeyCode.Escape);

       // 가정 동시에 4개이상 제출을 못한다
       
       if (num % 4 > 0)
       {
          _HorizP2 = Input.GetAxisRaw("HorizontalP2") * -1;
          _VertP2 = Input.GetAxisRaw("VerticalP2") * -1;
       }
       else
       {
          _HorizP2 = Input.GetAxisRaw("HorizontalP2");
          _VertP2 = Input.GetAxisRaw("VerticalP2");
       }

       if (num / 4 > 0)
       {
          _HorizP1 = Input.GetAxisRaw("HorizontalP1") * -1;
          _VertP1 = Input.GetAxisRaw("VerticalP1") * -1;
       }
       else
       {
          _HorizP1 = Input.GetAxisRaw("HorizontalP1");
          _VertP1 = Input.GetAxisRaw("VerticalP1");
       }
      
       
       
      // 입력받은 이동키 변수에 담기
      _moveDirectionP1 = new Vector3(_HorizP1, 0, _VertP1); 
      _moveDirectionP1.Normalize();
      _moveDirectionP2 = new Vector3(_HorizP2, 0, _VertP2);
      _moveDirectionP2.Normalize();
      
      _holdP1 = Input.GetKeyDown(KeyCode.G);  // 상호작용1 잡기 놓기
      _intaractDownP1 = Input.GetKeyDown(KeyCode.H);
      _intaractUpP1 = Input.GetKeyUp(KeyCode.H);

      _holdP2 = Input.GetKeyDown(KeyCode.Keypad0); // 잡기 놓기
      _intaractDownP2 = Input.GetKeyDown(KeyCode.KeypadPeriod);
      _intaractUpP2 = Input.GetKeyUp(KeyCode.KeypadPeriod);
      
   }
}
