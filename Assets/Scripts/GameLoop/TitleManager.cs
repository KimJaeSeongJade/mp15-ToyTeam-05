using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance { get; private set; }
    [SerializeField] private GameData _gameData;
    
    public List<Button> _buttons = new List<Button>(4);
    [SerializeField] private Image _p1Ready;
    [SerializeField] private Sprite _cabbage;
    [SerializeField] private Sprite _Uncabbage;
    [SerializeField] private GameObject _p1ReadyBG;
    [SerializeField] private Image _p2Ready;
    [SerializeField] private GameObject _p2ReadyBG;
    [SerializeField] private Sprite _tomato;
    [SerializeField] private Sprite _Untomato;
    
    [SerializeField] private GameObject _popHowToPlayUI;
    [SerializeField] private TextMeshProUGUI _howToPlayText;
    
    public GameData GameData => _gameData;
    private bool _P1Ready = false;
    private bool _P2Ready = false;
    private Button _selectedButton;
    private static int _buttonIndex = 0;


    // -----------------사운드 테스트


    [SerializeField] private GameObject _soundUI;



    // -----------------사운드 테스트

    private void Awake()
    {
        SetSingleton();
    }
    
    private void Start()
    {
        SoundManager.Instance.BGMPlay(BGMType.Title);
        _selectedButton = _buttons[_buttonIndex];
        StartCoroutine(StartUI());
    }

    private void Update()
    {
        GetkeySelect();
        OnClick();
        OnCloseUI();
    }
    
    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
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

    // 플레이어 1 게임 레디
    public void GameStartP1()
    {
        _P1Ready = !_P1Ready;

        if (_P1Ready)
        {
            _p1Ready.sprite = _Uncabbage;
            _p1ReadyBG.SetActive(true);
        }
        else
        {
            _p1Ready.sprite = _cabbage;
             _p1ReadyBG.SetActive(false);
        }

        if (_P1Ready && _P2Ready)
        {
            StartCoroutine(GameStartDelay());
        }
    }

    // 플레이어 2 게임 레디
    public void GameStartP2()
    {
        _P2Ready = !_P2Ready;

        if (_P2Ready)
        {
            _p2Ready.sprite = _Untomato;
            _p2ReadyBG.SetActive(true);
        }
        else
        {
            _p2Ready.sprite = _tomato;
            _p2ReadyBG.SetActive(false);
        }

        if (_P1Ready && _P2Ready)
        {
            StartCoroutine(GameStartDelay());
        }
    }

    // 약간 딜레이 후 게임 시작
    private IEnumerator GameStartDelay()
    {
        SoundManager.Instance.BGMStop(BGMType.Title);
        yield return new WaitForSeconds(0.5f);
        ResetGameData();
        if(_gameData.GameMode == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }

        InputManager.Instance.OnCookP1 -= GameStartP1;
        InputManager.Instance.OnCookP2 -= GameStartP2;
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

    //=========================================================
    private IEnumerator StartUI()
    {
        yield return new WaitForSeconds(1f);
        Selected();
    }

    // 지금 선택중인 버튼 찾기
    private void GetkeySelect()
    {
        if (_popHowToPlayUI.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_buttonIndex == _buttons.Count - 1)
            {
                _buttonIndex = -1;
            }

            _buttonIndex++;
            Selected();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_buttonIndex == 0)
            {
                _buttonIndex = _buttons.Count;
            }

            _buttonIndex--;
            Selected();
        }
    }

    // 선택중인 버튼 이펙트
    private void Selected()
    {
        _selectedButton = _buttons[_buttonIndex];
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

    // 선택중인 버튼 눌렀을 때
    private void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_buttonIndex == 0)
            {
                InputManager.Instance.OnCookP1 += GameStartP1;
                InputManager.Instance.OnCookP2 += GameStartP2;
                
                _popHowToPlayUI.SetActive(true);
                _howToPlayText.text = "동료 쉐프와 함께 식당을 운영합니다!\n3스타 식당이 되기 위해 더 많은 주문을 처리하세요.\n요리 준비가 되었다면 [요리]버튼을 눌러주세요.";
                _gameData.GameMode = 1;
            }
            else if (_buttonIndex == 1)
            {
                InputManager.Instance.OnCookP1 += GameStartP1;
                InputManager.Instance.OnCookP2 += GameStartP2;
                
                _popHowToPlayUI.SetActive(true);
                _howToPlayText.text = "옆 식당은 당신의 라이벌입니다!\n상대보다 더 빠르게 요리하고 주문을 처리하세요.\n요리 준비가 되었다면 [요리]버튼을 눌러주세요.";
                _gameData.GameMode = 0;
            }
            else if (_buttonIndex == 2)
            {
                // 세팅 -사운드
            }
            else
            {
               QuitGame();
            }
        }
    }

    // 팝업 끄기_ 키보드로
    private void OnCloseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _popHowToPlayUI.activeSelf)
        {
            _popHowToPlayUI.SetActive(false);
            InputManager.Instance.OnCookP1 -= GameStartP1;
            InputManager.Instance.OnCookP2 -= GameStartP2;
        }
    }


    public void OpenSound()
    {
        _soundUI.SetActive(true);
    }
    public void ClseSound()
    {
        _soundUI.SetActive(false);
    }

    public void SFXSound()
    {
        SoundManager.Instance.SFXPlay(SFXType.Dish);
    }
}
