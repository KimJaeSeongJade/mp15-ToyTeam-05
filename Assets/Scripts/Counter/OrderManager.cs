using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    [SerializeField] private List<Food> FoodPrefab = new List<Food>(5); // 요리들 라인업
    [SerializeField] private List<GameObject> FoodImageprefab = new List<GameObject>(5); // 주문서 UI 라인업
    
    [SerializeField] public List<Food> OrderList = new List<Food>(6); // 실제 주문서들
    // [SerializeField] private List<Transform> OrderPosition = new (6); // 주문서 UI 포지션
    
    [SerializeField] private Transform _OrderUi; // 주문서 클론 부모
    private int playerIndex;
    
    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        AddOrder();
        OrderUi();
    }

    private void Update()
    {
       // AddOrder();
    }
    
    //==================================================

    
    private void OrderUi()
    {
        for (int i = 0; i < OrderList.Count; i++)
        { 
            if(OrderList[i] == null) return;
            // 오더 리스트 i는 FoodPrefab의 몇 번째 인덱스에 있나? 찾기
            int index = FoodPrefab.IndexOf(OrderList[i]);
            Debug.Log("여기는 됨");
            if (index != -1)
            {
                Debug.Log("인스턴스 생성");
                Instantiate(FoodImageprefab[index], _OrderUi);
            }
        }
    }
   
    
    //====================================================
    private void AddOrder()
    {
        if (OrderList.Count < OrderList.Capacity)
        {
            RandomOrder();
        }
    }
    
    private void RandomOrder()
    {
        int Order = Random.Range(0, FoodPrefab.Count-1);
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