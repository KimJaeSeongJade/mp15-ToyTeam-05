using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOrderUI : MonoBehaviour
{
    private List<GameObject> orderList = new List<GameObject>();
    private int _index;
    
    public void RemoveOrderUi()
    {
        foreach (Transform child in transform)
        {
            orderList.Add(child.gameObject);
        }
        
        _index = OrderManager.Instance._successIndex;
        
        Destroy(orderList[_index]);
        //orderList[_index].SetActive(false);
        orderList.RemoveAt(_index);
        orderList.Clear();
    }
}
