using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
   private float _HorizP1 = Input.GetAxisRaw("HorizontalP1");
   private float _VertP1 = Input.GetAxisRaw("VerticalP1");
   
   private float _HorizP2 = Input.GetAxisRaw("HorizontalP2");
   private float _VertP2 = Input.GetAxisRaw("VerticalP2");
   
   private Vector3 _moveDirectionP1;
   private Vector3 _moveDirectionP2;

   // 플레이어 이동 관련 이벤트
   public event Action<Vector3> OnInputP1;
   public event Action<Vector3> OnInputP2;
   
   // 플레이어 상호작용 관련 이벤트
   public event Action OnIntaractP1; 
   public event Action OnIntaractP2; 
   //-------------------------------------------------------
   
   private void Awake()
   {
      SetSingleton();
   }

   private void Start()
   {
   }

   private void Update()
   {
      ReadInputs();
      
      // 이동 입력 받으면 해당 이벤트 구독중인 함수에 입력받은 백터값 넘김.
      OnInputP1?.Invoke(_moveDirectionP1);
      OnInputP2?.Invoke(_moveDirectionP2);
   }


   private void ReadInputs()
   {
      // 입력받은 이동키 변수에 담기
      _moveDirectionP1 = new Vector3(_HorizP1, 0, _VertP1);
      _moveDirectionP2 = new Vector3(_HorizP2, 0, _VertP2);
      
      bool _intaractP1 = Input.GetKeyDown(KeyCode.G);  // 잡기 놓기
      bool _skillP1 = Input.GetKeyDown(KeyCode.H);  // 조리 
      
      bool _intaractP2 = Input.GetKeyDown(KeyCode.Keypad0); // 잡기 놓기
      bool _skillP2 = Input.GetKeyDown(KeyCode.Keypad1);  // 조리
      
   }

}
