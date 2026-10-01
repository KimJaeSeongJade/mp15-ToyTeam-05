using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject _popupUI;
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Popup()
    {
        _popupUI.SetActive(true);
    }

    // 게임 종료하기
    public void QuitGame()
    {
#if UNITY_EDITOR
        // 유니티 에디터에서 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 실제 빌드된 게임에서 애플리케이션 종료
        Application.Quit();
#endif
    }
}
