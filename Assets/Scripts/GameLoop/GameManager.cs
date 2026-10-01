using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake() => SetSingleton();

    // 타이틀로 돌아가기
    public void TitleScene()
    {
        SceneManager.LoadScene(0);
    }
    
    // 게임시작씬으로 가기
    public void GameStart()
    {
        SceneManager.LoadScene(1);
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
    
    
    private void SetSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
