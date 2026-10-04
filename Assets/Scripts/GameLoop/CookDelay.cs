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
    private bool _isActivate;
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
            if(!_isActivate)
            {
                gameObject.SetActive(true);
                _isActivate = true;
            }
            _timer.fillAmount = _cookProgress / 100;
        }
        else
        {
            gameObject.SetActive(false);
            _isActivate = false;
            return;
        }
    }
}
