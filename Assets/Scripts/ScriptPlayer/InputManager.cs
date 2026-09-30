using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
   private bool _Player1Forword = Input.GetKeyDown(KeyCode.W);
   private bool _Player1Back = Input.GetKeyDown(KeyCode.S);
   private bool _Player1Right = Input.GetKeyDown(KeyCode.A);
   private bool _Player1Left = Input.GetKeyDown(KeyCode.D);
   
   
   private void Awake()
   {
      SetSingleton();
   }
   
   
   private void ReadGetKey()
   {
      
   }

   private void PlayerMovement()
   {
      
   }


}
