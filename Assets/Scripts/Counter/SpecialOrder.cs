using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private PlayerMovement _playerMovement;
    // ---------------------- 플레이어 슬로우
    
    
    private void Start()
    {
        _ink[0].enabled = false;
        _ink[1].enabled = false;
    }

    // int 플레이어 0 이면 P1 , 플레이어 1 이면 P2가 당함.
    public void RandomSpecial(int Damageplayer)
    {
        int _special = Random.Range(0, 4);
        switch (_special)
        {
            // 시야 방해하기
            case 0:
                StartCoroutine(Ink(Damageplayer));
                break;
            
            // 플레이어 조작 방해
            case 1:
                break;
            
            // 벨트 막힘
            case 2:
                StartCoroutine(BeltStop(Damageplayer));
                break;
            
            // 속도 둔화
            case 3:
                StartCoroutine(PlayerSlow(Damageplayer));
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


    private IEnumerator BeltStop(int Damageplayer)
    {
        if (Damageplayer == 0)
        {
            _beltStop1 = true;
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
            
            yield return new WaitForSeconds(3f);
            
        }
        else
        {
            
            yield return new WaitForSeconds(3f);
            
            
        }
    }
}
