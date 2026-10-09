using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SpecialOrder : MonoBehaviour
{
    [SerializeField] private GameObject[] _ink = new GameObject[2];
    [SerializeField] private Image _p1BugPos;
    [SerializeField] private Image _p2BugPos;
    [SerializeField] private Sprite[] _BugImage = new Sprite[3];
    
    // ---------------------- 벨트 스폰 X 관련
    [SerializeField] public bool _beltStop1 = false;
    [SerializeField] public bool _beltStop2 = false;
    // ---------------------- 벨트 스폰 X 관련

    // ---------------------- 플레이어 슬로우
    [SerializeField] private PlayerMovement _player1;
    [SerializeField] private PlayerMovement _player2;
    // ---------------------- 플레이어 슬로우


    private void Start()
    {
    }

    // ------------------------ 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(MixGetkey(0));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(MixGetkey(1));
        }
    }
    // ------------------------ 테스트

    // int 플레이어 0 이면 P1 , 플레이어 1 이면 P2가 당함.
    public void RandomSpecial(int Damageplayer)
    {
        int _special = Random.Range(0, 4);
        Debug.Log(_special);
        
        switch (_special)
        {
            // 시야 방해하기
            case 0:
                StartCoroutine(PlaySquid());
                StartCoroutine(Ink(Damageplayer));
                Debug.Log("시야방해");
                break;
            
            // 플레이어 조작 방해
            case 1:
                StartCoroutine(PlaySound());
                StartCoroutine(MixGetkey(Damageplayer));
                Debug.Log("조작방해");
                break;
            
            // 벨트 막힘
            case 2:
                StartCoroutine(PlaySound());
                StartCoroutine(BeltStop(Damageplayer));
                Debug.Log("벨트 막힘");
                break;
            
            // 속도 둔화
            case 3:
                StartCoroutine(PlaySound());
                StartCoroutine(PlayerSlow(Damageplayer));
                Debug.Log("둔화");
                break;
        }
    }

    private IEnumerator Ink(int Damageplayer)
    {
        Debug.Log("잉크 튀기기");
        if (Damageplayer != 1)
        {
            _ink[0].SetActive(true);
            yield return new WaitForSeconds(3f);
            _ink[0].GetComponent<Image>().color = new Color32(255, 255, 255, 230);
            yield return new WaitForSeconds(0.7f);
            _ink[0].GetComponent<Image>().color = new Color32(255, 255, 255, 200);
            yield return new WaitForSeconds(0.7f);
            _ink[0].GetComponent<Image>().color = new Color32(255, 255, 255, 170);
            yield return new WaitForSeconds(0.7f);
            _ink[0].GetComponent<Image>().color = new Color32(255, 255, 255, 140);
            _ink[0].SetActive(false);
            _ink[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            _ink[1].SetActive(true);
            yield return new WaitForSeconds(3f);
            _ink[1].GetComponent<Image>().color = new Color32(255, 255, 255, 230);
            yield return new WaitForSeconds(0.7f);
            _ink[1].GetComponent<Image>().color = new Color32(255, 255, 255, 200);
            yield return new WaitForSeconds(0.7f);
            _ink[1].GetComponent<Image>().color = new Color32(255, 255, 255, 170);
            yield return new WaitForSeconds(0.7f);
            _ink[1].GetComponent<Image>().color = new Color32(255, 255, 255, 140);
            _ink[1].SetActive(false);
            _ink[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    private IEnumerator MixGetkey(int Damageplayer)
    {
        Debug.Log("이동키 반전");
        if (Damageplayer == 0)
        {
            StartCoroutine(BugImage(_p1BugPos, _BugImage[0]));
            
            //InputManager.Instance._moveDirectionP1 *= -1;
            InputManager.Instance.num += 1;
            yield return new WaitForSeconds(3f);
            //InputManager.Instance._moveDirectionP1 *= -1;
            InputManager.Instance.num -= 1;
        }
        else
        {
            StartCoroutine(BugImage(_p2BugPos, _BugImage[0]));
            
            // InputManager.Instance._moveDirectionP2 *= -1;
            InputManager.Instance.num += 4;
            yield return new WaitForSeconds(3f);
            //InputManager.Instance._moveDirectionP2 *= -1;
            InputManager.Instance.num -= 4;
        }
    }

    private IEnumerator BeltStop(int Damageplayer)
    {
        if (Damageplayer == 0)
        {
            StartCoroutine(BugImage(_p1BugPos, _BugImage[1]));
           
            _beltStop1 = true;
            yield return new WaitForSeconds(10f);
            _beltStop1 = false;
        }
        else
        {
            StartCoroutine(BugImage(_p2BugPos, _BugImage[1]));
            
            _beltStop2 = true;
            yield return new WaitForSeconds(10f);
            _beltStop2 = false;
    
        }
    }

    private IEnumerator PlayerSlow(int Damageplayer)
    {
        if (Damageplayer == 0)
        {
            StartCoroutine(BugImage(_p1BugPos, _BugImage[2]));
            
            _player1.ChangeSpeed(100);
            yield return new WaitForSeconds(3f);
            _player1.ChangeSpeed(300);
        }
        else
        {
            StartCoroutine(BugImage(_p2BugPos, _BugImage[2]));
            
            _player2.ChangeSpeed(100);
            yield return new WaitForSeconds(3f);
            _player2.ChangeSpeed(300);

        }
    }

    private IEnumerator BugImage(Image Playerpos, Sprite BugImage)
    {
        Playerpos.sprite = BugImage;
        Playerpos.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Playerpos.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Playerpos.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Playerpos.gameObject.SetActive(false);
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.SFXPlay(SFXType.Debuff);
    }

    private IEnumerator PlaySquid()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.SFXPlay(SFXType.Squid);
    }
}
