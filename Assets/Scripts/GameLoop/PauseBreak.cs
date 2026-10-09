using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBreak : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    private bool _isPaused;
    [SerializeField] private Canvas _canvas;
    private PlayManager _playManager;
    [SerializeField] private Image[] _button = new Image[2];
    private bool _selected = true;
    private void Awake()
    {
        _playManager = GetComponent<PlayManager>();
    }

    private void Update()
    {
        GetkeySelect();
    }

    private void OnEnable() => _inputManager.OnPauseBreak += EscapeOpened;
    private void OnDisable() => _inputManager.OnPauseBreak -= EscapeOpened;
    
    
    // =============================================

    public void EscapeOpened()
    {
        Debug.Log("EscapeOpened");
        if (!_playManager.IsGameStart || _playManager.IsGameEnd) return;
        if (_isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        _canvas.enabled = true;
        _isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _canvas.enabled = false;
        _isPaused = false;
        Time.timeScale = 1;
    }

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
    
    //===============================================

    private void GetkeySelect()
    {
        if (!_isPaused) return;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
        {
            _selected = !_selected;
        }
        
        selected();
    }

    private void selected()
    {
        if(!_isPaused) return;
        if (_selected)
        {
            _button[1].color = new Color32(255, 255, 255,0);
            _button[0].color = new Color32(255, 255, 255, 255);
            if (Input.GetKey(KeyCode.Return))
            {
                Resume();
            }
        }
        else if (!_selected)
        {
            _button[0].color = new Color32(255, 255, 255,0);
            _button[1].color = new Color32(255, 255, 255, 255);
            if (Input.GetKey(KeyCode.Return))
            {
                QuitGame();
            }
        }
    }


}
