using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlay;
    [SerializeField] private TextMeshProUGUI _howToPlayText;
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
        GetkeySelect();
        OnClick();
        CloseUI();
    }

    private IEnumerator StartUI()
    {
        yield return new WaitForSeconds(1f);
        Selected();
    }

    private void GetkeySelect()
    {
        if (_howToPlay.activeSelf) return;
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
                _howToPlay.SetActive(true);
                _howToPlayText.text = "동료 쉐프와 함께 식당을 운영합니다!\n3스타 식당이 되기 위해 더 많은 주문을 처리하세요.\n요리 준비가 되었다면 [조리]버튼을 눌러주세요.";
                
            }
            else if (_index == 1)
            {
                _howToPlay.SetActive(true);
                _howToPlayText.text = "우리는 서로 경쟁 식당입니다!\n상대보다 더 빠르게 요리하고 주문을 처리하세요.\n요리 준비가 되었다면 [조리]버튼을 눌러주세요.";
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

    private void CloseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _howToPlay.activeSelf)
        {
            _howToPlay.SetActive(false);
        }
    }
}
