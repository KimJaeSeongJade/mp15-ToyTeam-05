using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    [SerializeField] private List<Food> FoodPrefab = new List<Food>(5); // 요리들 배열
    [SerializeField] public List<GameObject> FoodImageprefab = new List<GameObject>(5); // 주문서 UI 라인업
    
    [SerializeField] public List<Food> OrderList = new List<Food>(6); // 주문서 라인업
    [SerializeField] public List<Transform> OrderPosition = new (6); // 주문서 UI 포지션

    private int playerIndex;
    
    private void Awake()
    {
        SetSingleton();
    }
    
    private void Update()
    {
        AddOrder();
    }
    
    //==================================================

    
    private void OrderUi()
    {
        for (int i = 0; i < OrderList.Count; i++)
        { 
            if(OrderList[i] == null) return;
            // 오더 리스트 i는 FoodPrefab의 몇 번째 인덱스에 있나? 찾기
            int index = FoodPrefab.IndexOf(OrderList[i]);
            
            Instantiate(FoodImageprefab[index], OrderPosition[i].position, Quaternion.identity);
        }
    }
    // 삭제 상황도 해야함
    
    //====================================================
    private void AddOrder()
    {
        if (OrderList.Count < OrderList.Capacity)
        {
            RandomOrder(); 
            OrderUi();
        }
    }
    
    private void RandomOrder()
    {
        int Order = Random.Range(0, FoodPrefab.Capacity-1);
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