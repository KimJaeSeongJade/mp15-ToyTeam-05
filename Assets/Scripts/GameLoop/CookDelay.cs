using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CookDelay : MonoBehaviour
{
    [SerializeField] private Image _timer;
    private Cookware _cookware;
    private Canvas _canvas;
    private bool _isActivate;
    // 필드 프로퍼티

    private void Awake()
    {
        _cookware = GetComponentInParent<Cookware>();
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
    }
    
// awake시 호출
    private void Start()
    {
        _isActivate = false;
        _canvas.enabled = false;
    }

    private void Update() => StateTimer();
    
    private void StateTimer()
    {
        if (_cookware.CookProgress >= 0)
        {
            if(!_isActivate)
            {
                _canvas.enabled = true;
                _isActivate = true;
            }
            _timer.fillAmount = _cookware.CookProgress / 100;
        }
        else
        {
            _canvas.enabled = false;
            _isActivate = false;
            return;
        }
    }
}
