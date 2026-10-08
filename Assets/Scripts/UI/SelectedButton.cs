using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField] private Animator _dropDown;
    public List<Button> _buttons = new List<Button>(4);

    private static int _index = 0;
    private Button _selectedButton;

    private void Start()
    {
        _selectedButton = _buttons[_index];
        StartCoroutine(StartUI());
    }
    
    private void Update()
    {
        Getkey();
    }

    private IEnumerator StartUI()
    {
        yield return new WaitForSeconds(1f);
        Selected();
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
                t.transform.SetAsLastSibling();
                t.GetComponent<RectTransform>().localScale = new Vector3(6f, 6f, 1f);
                t.GetComponent<Image>().color = Color.white;
                t.GetComponent<Outline>().enabled = true;
            }
            else
            {
                // 기본 상태
                t.GetComponent<RectTransform>().localScale = new Vector3(5f, 5f, 1f);
                t.GetComponent<Image>().color = new Color32(255, 227, 192, 255);
                t.GetComponent<Outline>().enabled = false;
            }
        }
    }

    private void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_index == 0)
            {
                // 협동모드 -하우투
            }
            else if (_index == 1)
            {
                // 경쟁모드 -하우투
            }
            else if (_index == 2)
            { 
                // 세팅 -사운드
            }
            else
            {
                TitleManager.Instance.QuitGame();
            }
        }
    }
}
