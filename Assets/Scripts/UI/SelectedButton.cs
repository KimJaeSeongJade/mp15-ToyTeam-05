using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField] private List<Canvas> _info;
    public List<Button> _buttons = new List<Button>(4);

    private static int _index = 0;
    private Button _selectedButton;

    private void Start()
    {
        _selectedButton = _buttons[_index];
    }
    
    private void Update()
    {
        Getkey();
        OnClick();
    }

    private void Getkey()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_index == _buttons.Count - 1)
            {
                _index = -1;
            }

            _index++;
            Selected();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_index == 0)
            {
                _index = _buttons.Count;
            }

            _index--;
            Selected();
        }
    }


    private void Selected()
    {
        _selectedButton = _buttons[_index];
        foreach (Button t in _buttons)
        {
            if (_selectedButton == t)
            {
                // 선택 상태
                t.GetComponent<RectTransform>().localScale = new Vector3(7f, 7f, 1.3f);
                t.GetComponent<Image>().color = Color.red;

            }
            else
            {
                // 기본 상태
                t.GetComponent<RectTransform>().localScale = new Vector3(5f, 5f, 1f);
                t.GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_index == 0)
            {
                TitleManager.Instance.HowToPopup();
            }
            else if (_index == 1)
            {
                TitleManager.Instance.QuitGame();
            }
            else if (_index == 2)
            { 
                TitleManager.Instance.CreditPopup();
            }
            else
            {
                // 셋팅 팝업
            }
        }
    }
}
