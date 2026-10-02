using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _resultPopup;
    [SerializeField] private TextMeshProUGUI _startCountText;


    [SerializeField] private TextMeshProUGUI _player1Score;
    [SerializeField] private TextMeshProUGUI _player1Bilge;


    [SerializeField] private TextMeshProUGUI _player2Score;
    [SerializeField] private TextMeshProUGUI _player2Bilge;



    [SerializeField] private GameObject _player1WinUI;
    [SerializeField] private GameObject _player1LoseUI;

    [SerializeField] private GameObject _player2WinUI;
    [SerializeField] private GameObject _player2LoseUI;

    private bool _isGameStart;
    private bool _isGameEnd;

    private void Start()
    {
        StartCoroutine(StartCountDown());
        StartCoroutine(TextColorChange());
    }

    private void Update()
    {
        if (!_isGameStart || _isGameEnd) return;

        GameManager.Instance.GameData.GameTimeLeft -= Time.deltaTime;

        if (GameManager.Instance.GameData.GameTimeLeft <= 0f)
        {
            GameManager.Instance.GameData.GameTimeLeft = 0f;
            GameEnd();
        }

        _timeText.text = Mathf.CeilToInt(GameManager.Instance.GameData.GameTimeLeft).ToString();
    }
    //----------------------

    private IEnumerator TextColorChange()
    {
        yield return new WaitForSeconds(GameManager.Instance.GameData.GameTimeLeft - 6f);
        _timeText.color = Color.red;
    }

    //----------------------
    
    private IEnumerator StartCountDown()
    {
        _isGameStart = false;

        _startCountText.gameObject.SetActive(true);

        _startCountText.text = "3";
        yield return new WaitForSeconds(1f);

        _startCountText.text = "2";
        yield return new WaitForSeconds(1f);

        _startCountText.text = "1";
        yield return new WaitForSeconds(1f);

        _startCountText.text = "START!";
        yield return new WaitForSeconds(1f);

        _startCountText.gameObject.SetActive(false);

        _isGameStart = true;
    }


    private void PlayerWin()
    {
        _player1Score.text = "player1 Score : " + GameManager.Instance.GameData.Player1Score.ToString();
        _player1Bilge.text = "player1 Bilge : " + GameManager.Instance.GameData.Player1Food.ToString();

        _player2Score.text = "player2 Score : " + GameManager.Instance.GameData.Player2Score.ToString();
        _player2Bilge.text = "player2 Bilge : " + GameManager.Instance.GameData.Player2Food.ToString();

        int player1Score = GameManager.Instance.GameData.Player1Score;
        int player2Score = GameManager.Instance.GameData.Player2Score;

        if (player1Score > player2Score)
        {
            _player1WinUI.SetActive(true);
            _player2LoseUI.SetActive(true);
        }
        else if (player1Score < player2Score)
        {
            _player1LoseUI.SetActive(true);
            _player2WinUI.SetActive(true);
        }
        // 테스트
        else
        {
            _player1WinUI.SetActive(true);
            _player2WinUI.SetActive(true);
        }
    }



    private void GameEnd()
    {
        _isGameEnd = true;
        _resultPopup.SetActive(true);
        PlayerWin();

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        GameManager.Instance.ResetGameData();

        SceneManager.LoadScene(1);
    }
    public void TitleScene()
    {
        GameManager.Instance.TitleScene();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
