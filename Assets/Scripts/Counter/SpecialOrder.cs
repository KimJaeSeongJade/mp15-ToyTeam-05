using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SpecialOrder : MonoBehaviour
{
    [SerializeField] private Image[] _ink = new Image[2];
    
    // ---------------------- 벨트 스폰 X 관련
    [SerializeField] private GameObject[] _stopImage = new GameObject[4];
    [SerializeField] public bool _beltStop1 = false;
    [SerializeField] public bool _beltStop2 = false;
    // ---------------------- 벨트 스폰 X 관련

    // ---------------------- 플레이어 슬로우
    [SerializeField] private PlayerMovement _player1;
    [SerializeField] private PlayerMovement _player2;
    // ---------------------- 플레이어 슬로우


    private void Start()
    {
        /*_ink[0].enabled = false;
        _ink[1].enabled = false;*/
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
        Debug.Log("스페셜 오더");
        
        int _special = Random.Range(0, 4);
        Debug.Log(_special);
        
        switch (_special)
        {
            // 시야 방해하기
            case 0:
                StartCoroutine(Ink(Damageplayer));
                Debug.Log("시야방해");
                break;
            
            // 플레이어 조작 방해
            case 1:
                StartCoroutine(MixGetkey(Damageplayer));
                Debug.Log("조작방해");
                break;
            
            // 벨트 막힘
            case 2:
                StartCoroutine(BeltStop(Damageplayer));
                Debug.Log("벨트 막힘");
                break;
            
            // 속도 둔화
            case 3:
                StartCoroutine(PlayerSlow(Damageplayer));
                Debug.Log("둔화");
                break;
        }
    }

    private IEnumerator Ink(int Damageplayer)
    {
        if (Damageplayer == 0)
        {
            _ink[0].enabled = true;
            yield return new WaitForSeconds(3f);
            _ink[0].enabled = false;
        }
        else
        {
            _ink[1].enabled = true;
            yield return new WaitForSeconds(3f);
            _ink[1].enabled = false;
        }
    }

    private IEnumerator MixGetkey(int Damageplayer)
    {
        if (Damageplayer == 0)
        {
            //InputManager.Instance._moveDirectionP1 *= -1;
            InputManager.Instance.num += 1;
            yield return new WaitForSeconds(3f);
            //InputManager.Instance._moveDirectionP1 *= -1;
            InputManager.Instance.num -= 1;
        }
        else
        {
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
            _beltStop1 = true;
            // 스폰안된다고 표시할 이미지???
            //_stopImage[0].SetActive(true);
            //_stopImage[1].SetActive(true);
            yield return new WaitForSeconds(3f);
            _beltStop1 = false;
            //_stopImage[0].SetActive(false);
            //_stopImage[1].SetActive(false);
        }
        else
        {
            _beltStop2 = true;
            //_stopImage[2].SetActive(true);
            //_stopImage[3].SetActive(true);
            yield return new WaitForSeconds(3f);
            _beltStop2 = false;
            //_stopImage[2].SetActive(false);
            //_stopImage[3].SetActive(false);
        }
    }

    private IEnumerator PlayerSlow(int Damageplayer)
    {
        if (Damageplayer == 0)
        {
            _player1.ChangeSpeed(100);
            yield return new WaitForSeconds(3f);
            _player1.ChangeSpeed(300);
        }
        else
        {
            _player2.ChangeSpeed(100);
            yield return new WaitForSeconds(3f);
            _player2.ChangeSpeed(300);

        }
    }
}
