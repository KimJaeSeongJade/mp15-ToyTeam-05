using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBreak : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    private bool _isPaused;
    [SerializeField] private Canvas _canvas;
    private PlayManager _playManager;

    private void Awake()
    {
        _playManager = GetComponent<PlayManager>();
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


}
