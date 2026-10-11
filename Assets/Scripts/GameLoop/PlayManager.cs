using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{

    [SerializeField] private GameData _gameData;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _resultPopup;
    [SerializeField] private Image _countImage;
    [SerializeField] private Image _startImage;
    [SerializeField] private Sprite[] _count1 = new Sprite[4]; // 협동
    [SerializeField] private Sprite[] _count0 = new Sprite[4]; // 경쟁


    // --------------------------------------------------- 경쟁

    [Header("경쟁전용")]
    [SerializeField] private TextMeshProUGUI _player1Score;
    [SerializeField] private TextMeshProUGUI _p1InGameScore;
    [SerializeField] private TextMeshProUGUI _player1Bilge;

    [SerializeField] private TextMeshProUGUI _player2Score;
    [SerializeField] private TextMeshProUGUI _p2InGameScore;
    [SerializeField] private TextMeshProUGUI _player2Bilge;



    [SerializeField] private GameObject _player1WinUI;
    [SerializeField] private GameObject _player1LoseUI;

    [SerializeField] private GameObject _player2WinUI;
    [SerializeField] private GameObject _player2LoseUI;
    [SerializeField] private GameObject _resultButton;

    // --------------------------------------------------- 경쟁



    // --------------------------------------------------- 협동

    [Header("협동전용")] 
    //[SerializeField] private GameObject _coopResult;
    [SerializeField] private TextMeshProUGUI _coopScore;
    [SerializeField] private TextMeshProUGUI _coopBilge;

    [SerializeField] private Image _StarGage;
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;

    [SerializeField] private GameObject _ingamestar1;
    [SerializeField] private GameObject _ingamestar2;
    [SerializeField] private GameObject _ingamestar3;

    [SerializeField] private int _star1Score;
    [SerializeField] private int _star2Score;
    [SerializeField] private int _star3Score;

    // --------------------------------------------------- 협동
    private bool _isGameStart;
    private bool _isGameEnd;
    private bool _isGaugeMax;
    
    public bool IsGameStart => _isGameStart;
    public bool IsGameEnd => _isGameEnd;

    private void Start()
    {
        SoundManager.Instance.BGMPlay(BGMType.Game);
        // 경쟁 협동 동일
        StartCoroutine(StartCountDown());
        StartCoroutine(TextColorChange());
    }

    private void Update()
    {
        if (!_isGameStart || _isGameEnd) return;

        _gameData.GameTimeLeft -= Time.deltaTime;

        if (_gameData.GameTimeLeft <= 0f)
        {
            _gameData.GameTimeLeft = 0f;
            StartCoroutine(GameEnd());
        }

        InGameScore();

        _timeText.text = Mathf.CeilToInt(_gameData.GameTimeLeft).ToString();
    }

    private IEnumerator TextColorChange()
    {
        yield return new WaitForSeconds(_gameData.GameTimeLeft - 6f);
        _timeText.color = Color.red;
    }


    private void InGameScore()
    {
        // 경쟁
        if (_gameData.GameMode == 0)
        {
            _p1InGameScore.text = _gameData.Player1Score.ToString();
            _p2InGameScore.text = _gameData.Player2Score.ToString();
        }
        // 협동
        else
        {
            // 합쳐진 점수
            int PlayerScore = _gameData.Player1Score + _gameData.Player2Score;
            
            _p1InGameScore.text = PlayerScore.ToString(); // 합쳐진 점수표시
            _player1Bilge.text = (_gameData.Player1Food + _gameData.Player2Food).ToString();

            // 점수 게이지 채워짐
            if (!_isGaugeMax)
            {
                _StarGage.fillAmount = PlayerScore / (float)_star3Score;
            }

            if (PlayerScore > (float)_star3Score)
            {
                _StarGage.fillAmount = 1;
                _isGaugeMax = true;
            }

            /*if (PlayerScore < _star3Score + 1)
            {
                _StarGage.fillAmount = PlayerScore / _star3Score;
            }
            else if (PlayerScore > _star3Score)
            {
                _StarGage.fillAmount = 1;
            }*/
            
            // 점수따라 별 생성
            if (PlayerScore < _star1Score) return;
            else if (_star1Score <= PlayerScore && PlayerScore < _star2Score)
            {
                _ingamestar1.SetActive(true);
            }
            else if (_star2Score <= PlayerScore && PlayerScore < _star3Score)
            {
                _ingamestar2.SetActive(true);
            }
            else
            {
                _ingamestar3.SetActive(true);
            }
        }
    }

    // 카운트다운
    public IEnumerator StartCountDown()
    {
        SoundManager.Instance.SFXPlay(SFXType.GameStart);
        _isGameStart = false;

        if (_gameData.GameMode == 0) // 경쟁모드
        {
            _countImage.gameObject.SetActive(true);

            _countImage.sprite = _count0[0];
            yield return new WaitForSeconds(1f);

            _countImage.sprite = _count0[1];
            yield return new WaitForSeconds(1f);

            _countImage.sprite = _count0[2];
            yield return new WaitForSeconds(1f);

            _countImage.gameObject.SetActive(false);
            _startImage.gameObject.SetActive(true);
            _startImage.sprite = _count0[3];
            yield return new WaitForSeconds(1f);

            _startImage.gameObject.SetActive(false);

            _isGameStart = true;
        }
        if (_gameData.GameMode == 1) // 협동모드
        {
            _countImage.gameObject.SetActive(true);

            _countImage.sprite = _count1[0];
            yield return new WaitForSeconds(1f);

            _countImage.sprite = _count1[1];
            yield return new WaitForSeconds(1f);

            _countImage.sprite = _count1[2];
            yield return new WaitForSeconds(1f);

            _countImage.gameObject.SetActive(false);
            _startImage.gameObject.SetActive(true);
            _startImage.sprite = _count1[3];
            yield return new WaitForSeconds(1f);

            _startImage.gameObject.SetActive(false);

            _isGameStart = true;
        }
    }


    private void PlayerWin()
    {
        ResultScoreUI();

        int player1Score = _gameData.Player1Score;
        int player2Score = _gameData.Player2Score;

        int player1Food = _gameData.Player1Food;
        int player2food = _gameData.Player2Food;

        // 경쟁
        if (_gameData.GameMode == 0) 
        {
            if (player1Score > player2Score) Player1Win();
            else if (player1Score < player2Score) Player2Win();
            else
            {
                if (player1Food > player2food) Player1Win();
                else if (player1Food < player2food) Player2Win();
                else PlayerDraw();
            }
        }
        // 협동
        else
        {
            // 구현 뭐할지 상의
            if (_gameData.Player1Score + _gameData.Player2Score >= _star1Score)
            {
                _star1.SetActive(true);
            }
            if (_gameData.Player1Score + _gameData.Player2Score >= _star2Score)
            {
                _star2.SetActive(true);
            }
            if (_gameData.Player1Score + _gameData.Player2Score >= _star3Score)
            {
                _star3.SetActive(true);
            }
            
        }

        
    }

    private void ResultScoreUI()
    {
        // 경쟁
        if(_gameData.GameMode == 0)
        {
            _player1Score.text = "Score : " + _gameData.Player1Score.ToString();
            _player1Bilge.text = "Bilge : " + _gameData.Player1Food.ToString();

            _player2Score.text = "Score : " + _gameData.Player2Score.ToString();
            _player2Bilge.text = "Bilge : " + _gameData.Player2Food.ToString();
        }
        // 협동
        else
        {
            _coopScore.text = "Score : " + (_gameData.Player1Score + _gameData.Player2Score).ToString();
            _coopBilge.text = "Bilge : " + (_gameData.Player1Food + _gameData.Player2Food).ToString();
        }
        
    }

    // ------------------------------------------- 경쟁 전용
    private void Player1Win()
    {
        _player1WinUI.SetActive(true);
        _player2LoseUI.SetActive(true);
    }

    private void Player2Win()
    {
        _player1LoseUI.SetActive(true);
        _player2WinUI.SetActive(true);
    }

    private void PlayerDraw()
    {
        _player1WinUI.SetActive(true);
        _player2WinUI.SetActive(true);
    }
    // ------------------------------------------- 경쟁 전용



    private IEnumerator GameEnd()
    {
        SoundManager.Instance.SFXPlay(SFXType.Result);
        _isGameEnd = true;
        _resultPopup.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        PlayerWin();
        
        yield return new WaitForSeconds(1.5f);
        if(_gameData.GameMode == 0)
        {
            _resultButton.SetActive(true);
        }
        
        
        
        SoundManager.Instance.BGMStop(BGMType.Game);
        SoundManager.Instance.BGMPlay(BGMType.End);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        ResetGameData();

        if(_gameData.GameMode == 0)
        { 
            SceneManager.LoadScene(1); 
        }
        else if(_gameData.GameMode == 1)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void ResetGameData()
    {
        _gameData.Player1Score = 0;
        _gameData.Player2Score = 0;

        _gameData.Player1Food = 0;
        _gameData.Player2Food = 0;

        _gameData.GameTimeLeft = GameData.START_GAMETIME;
    }

    public void TitleScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
