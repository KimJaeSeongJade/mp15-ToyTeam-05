using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
  [SerializeField] private Canvas _Ready;
  [SerializeField] private Image[] _page1 = new Image[2];
  [SerializeField] private Image _page3;
  [SerializeField] private Sprite[] _cook;

  private int _pageNum = 0;
  private bool _boolpage1 =  false;
  private void OnEnable()
  {
    InputManager.Instance.OnIntaractP1 += Page1;
    InputManager.Instance.OnIntaractP2 += Page1;
    InputManager.Instance.OnCookP1 += Page3;
    InputManager.Instance.OnCookP2 += Page3;
  }

  private void Update()
  {
       Naxt();
  }

  private void Naxt()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      InputManager.Instance.OnIntaractP1 -= Page1;
      InputManager.Instance.OnIntaractP2 -= Page1;
      InputManager.Instance.OnCookP1 -= Page3;
      InputManager.Instance.OnCookP2 -= Page3;
    }
  }

  private void Page1()
  {
    _boolpage1 = !_boolpage1;
    
    if (_boolpage1)
    {
      _page1[0].gameObject.SetActive(false);
      _page1[1].gameObject.SetActive(true);
    }
    else
    {
      _page1[1].gameObject.SetActive(false);
      _page1[0].gameObject.SetActive(true);
    }
  }
  
  private void Page3()
  {
    if (_pageNum >= _cook.Length)
    {
      _pageNum = 0;
    }
    _pageNum++;
    _page3.sprite = _cook[_pageNum];
  }
}
