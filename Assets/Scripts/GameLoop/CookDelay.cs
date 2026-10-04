using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookDelay : MonoBehaviour
{
    [SerializeField] private Image _timer;
    private Cookware _cookware;
    private int _cookProgress { get; set; }
// 필드 프로퍼티

    private void Awake()
    {
        _cookware = GetComponent<Cookware>();
        _cookProgress = _cookware.CookProgress;
    }
    
// awake시 호출
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update() => StateTimer();
    
    private void StateTimer()
    {
        if (_cookProgress >= 0)
        {
            gameObject.SetActive(true);
            _timer.fillAmount = _cookProgress / 100;

            if (_cookProgress >= 100)
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
