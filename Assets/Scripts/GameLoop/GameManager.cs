using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameData _gameData;
    [SerializeField] private Image _p1Ready;
    [SerializeField] private Image _p2Ready;
    
    public GameData GameData => _gameData;
    private bool _P1Ready = false;
    private bool _P2Ready = false;

    private void Awake() => SetSingleton();

    private void temp1() => GameStartP1(_p1Ready);
    private void temp2() => GameStartP2(_p2Ready);
    

    private void OnEnable()
    {
        InputManager.Instance.OnIntaractP1 += temp1;
        InputManager.Instance.OnCookP1 += temp1;
        InputManager.Instance.OnIntaractP2 += temp2;
        InputManager.Instance.OnCookP2 += temp2;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnIntaractP1 -= temp1;
        InputManager.Instance.OnCookP1 -= temp1;
        InputManager.Instance.OnIntaractP2 -= temp2;
        InputManager.Instance.OnCookP2 -= temp2;
    }

    // 타이틀로 돌아가기
    public void TitleScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    // 게임시작씬으로 가기
    public void GameStart()
    {
            ResetGameData();
            SceneManager.LoadScene(1);
    }
   
    public void GameStartP1(Image _ready)
    {
        _P1Ready = !_P1Ready;
        
        if (_P1Ready)
        {
            _ready.color = new Color32(11, 207, 98, 255);
        }
        else
        {
            _ready.color = new Color32(255,140,85,255);
        }
        
        if (_P1Ready && _P2Ready)
        {
            StartCoroutine(GameStartDelay());
        }
    }
    
    public void GameStartP2(Image _ready)
    {
        _P2Ready = !_P2Ready;
        
        if (_P2Ready)
        {
            _ready.color = new Color32(11, 207, 98, 255);
        }
        else
        {
            _ready.color = new Color32(255,140,85,255);
        }
        
        if (_P1Ready && _P2Ready)
        {
            StartCoroutine(GameStartDelay());
        }
    }

    private IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ResetGameData();
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


    public void ResetGameData()
    {
        _P1Ready = false;
        _P2Ready = false;
        
        _gameData.Player1Score = 0;
        _gameData.Player2Score = 0;

        _gameData.Player1Food = 0;
        _gameData.Player2Food = 0;
        
        _gameData.GameTimeLeft = GameData.START_GAMETIME;
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
