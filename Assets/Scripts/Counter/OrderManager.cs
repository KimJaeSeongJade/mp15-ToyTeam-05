using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    // 주문서 UI띄우고 밀리는건 UI 구현할때 하겠습니다!
    [SerializeField] private Food[] FoodPrefab = new Food[5]; // 요리들 배열
    [SerializeField] public List<Food> OrderList = new List<Food>(6); // 주문서 라인업
    [SerializeField] public List<GameObject> FoodImageprefab = new List<GameObject>(5);
    [SerializeField] public List<Transform> OrderPosition = new (6);

    private int playerIndex;
    
    private void Awake()
    {
        SetSingleton();
    }
    
    private void Update()
    {
        AddOrder();
    }

    private void AddOrder()  // 주문공간 비면 추가해라
    {
        if (OrderList.Count < OrderList.Capacity)
        {
            RandomOrder();
        }
    }
    
    private void RandomOrder()
    {
        int Order = Random.Range(0, FoodPrefab.Length-1);
        OrderList.Add(FoodPrefab[Order]);
    }

// =================================== 테스트용 (버튼 누르면 랜덤 삭제)
    /*private void RemoveOrder()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int Order = Random.Range(0, OrderList.Capacity - 1);
            Debug.Log(Order);
            OrderList.RemoveAt(Order);
        }
    }*/
}
