using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject _popupUI;
    public void GameStart()
    {
        GameManager.Instance.GameStart();
    }

    public void Popup()
    {
        _popupUI.SetActive(true);
    }

    public void ClosePopup()
    {
        _popupUI.SetActive(false);
    }

    // 게임 종료하기
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
