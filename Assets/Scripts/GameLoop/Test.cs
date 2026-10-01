using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
        [SerializeField] private Food[] FoodPrefab = new Food[5]; // 요리들 배열
        public List<Food> OrderList = new List<Food>(6); // 주문서 라인업
        
        
        
        // 보스 패턴처럼 하면?
        // 레시피마다 시간이 있어
        // 시간이 되면 관리하는 큐에 집어넣어
        // 근데 큐에 집어 넣을때
        
        // 지금 프리팹을 넣고 있네?
        // 파괴되는 시간을 가지고 있다면??
        
        // 파괴되도 원래 레시피애는 그걸 가지고있나?
        
        // 파괴되는 시간도 같이 넣어
        // 파괴되는 시간이 되면 그 레시피는 파괴해
        
        
        
        private void Update()
        {
            AddOrder();
            // RemoveOrder();
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
        private void RemoveOrder()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int Order = Random.Range(0, OrderList.Capacity - 1);
                Debug.Log(Order);
                OrderList.RemoveAt(Order);
            }
        }
}
