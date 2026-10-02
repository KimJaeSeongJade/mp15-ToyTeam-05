using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    // 버릴시 점수 깎이는 것은 나중에 추가구현
    // [SerializeField] private int _DiscountScore;
    public GameData _gameData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            Destroy(other);
        }
    }
}
