using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialOrder : MonoBehaviour
{
    [SerializeField] private Image[] _ink = new Image[2];

    private void Start()
    {
        _ink[0].enabled = false;
        _ink[1].enabled = false;
    }

    // int 플레이어 0 이면 P1 , 플레이어 1 이면 P2가 당함.
    public void RandomSpecial(int Demageplayer)
    {
        int _special = Random.Range(0, 4);
        switch (_special)
        {
            // 시야 방해하기
            case 0:
                StartCoroutine(Ink(Demageplayer));
                break;
            
            // 플레이어 조작 방해
            case 1:
                break;
            
            // 벨트 막힘
            case 2:
                break;
            
            // 속도 둔화
            case 3:
                break;
        }
    }

    private IEnumerator Ink(int Demageplayer)
    {
        if (Demageplayer == 0)
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
}
