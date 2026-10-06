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
    private int _cookProgress;
    private bool _isActivate;
// 필드 프로퍼티

    private void Awake()
    {
        _cookware = GetComponentInParent<Cookware>();
        _canvas = GetComponent<Canvas>();
        Debug.Log(_cookware.name);
        _cookProgress = _cookware.CookProgress;
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
        Debug.Log("A1");
        if (_cookProgress >= 0)
        {
            Debug.Log("A2");
            if(!_isActivate)
            {
                Debug.Log("A3");
                _canvas.enabled = true;
                _isActivate = true;
            }
            _timer.fillAmount = _cookProgress / 100;
        }
        else
        {
            _canvas.enabled = false;
            Debug.Log(_cookware.CookProgress);
            _isActivate = false;
            return;
        }
    }
}
