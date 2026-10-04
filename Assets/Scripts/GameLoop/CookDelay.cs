using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookDelay : MonoBehaviour
{
    [SerializeField] private Image _timer;
    [SerializeField] private int _State;  // 여기 나중에 int값 상태 넘겨주는 스크립트로 바꿔주세요!

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update() => StateTimer();
    
    private void StateTimer()
    {
        if (_State >= 0)
        {
            gameObject.SetActive(true);
            _timer.fillAmount = _State / 100;

            if (_State >= 100)
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
