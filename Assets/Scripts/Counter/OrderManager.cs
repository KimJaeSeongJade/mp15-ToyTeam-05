using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    [SerializeField] private List<Food> FoodPrefab = new List<Food>(6); // 요리들 라인업
    [SerializeField] private List<GameObject> FoodImageprefab = new List<GameObject>(6); // 주문서 UI 라인업
    
    [SerializeField] public List<Food> OrderList = new List<Food>(6); // 실제 주문서들
    
    [SerializeField] private Transform _OrderUi; // 주문서 클론 부모
    public int _successIndex;
    
    private void Awake() => SetSingleton();
    
    private void Update()
    {
        AddOrder();
    }
    
    private void AddOrder()
    {
        if (OrderList.Count < OrderList.Capacity)
        {
            RandomOrder();
            AddOrderUi();
        }
    }
    
    private void AddOrderUi()
    {
            if(OrderList[OrderList.Count-1] == null) return;
            
            // 마지막꺼 인덱스 번호 라인업안에서 찾고
            int index = FoodPrefab.IndexOf(OrderList[OrderList.Count-1]); 
            if (index != -1)
            {
                Instantiate(FoodImageprefab[index], _OrderUi);
            }
    }
    
    private void RandomOrder()
    {
        int Order = Random.Range(0, FoodPrefab.Count);
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