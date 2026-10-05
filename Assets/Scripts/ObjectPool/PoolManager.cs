using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 처음에 생성할 오브젝트풀 수
    [SerializeField] private int _objectPoolSize = 10;
    
    // 오브젝트풀로 만들 음식들
    [SerializeField] private Food[] _foodObject;
    
    public static PoolManager Instance;

    // 풀 담당을 하는 리스트들
    private List<Food>[] pools;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        FillPool();
    }

    private void FillPool()
    {
        // 4개면 4개 리스트 만들어 [양배우] [토마토] [고기] [양파]
        pools = new List<Food>[_foodObject.Length];
        
        // i = 0 양배추일때
        for (int i = 0; i < _foodObject.Length; i++)
        {
            // 풀 담당할 리스트를 만들고
            // 양배추 풀 리스트를 만들고
            pools[i] = new List<Food>();
            
            // 리스트에 처음 생성할 오브젝트 수만큼(10개) 담고
            for (int j = 0; j < _objectPoolSize; j++)
            {
                FillFood(i);
            }
        }
        
    }

    // 양배추 리스트에 푸드를 담는다
    private void FillFood(int index)
    {
        Food foodInstance = Instantiate(_foodObject[index]);
        pools[index].Add(foodInstance);
        foodInstance.gameObject.SetActive(false);
    }

    
    
    public Food Get(Food food)
    {
        int index = Array.IndexOf(_foodObject, food);
        Food select =  null;

        // ... 다른 스크립트에서 사용할때 몇번째 음식을 소환할래요 라고
        // 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (Food o in pools[index])
        { 
            // 비활성화된 상태
            if (!o.gameObject.activeSelf)
            {
                // 변수에 할당
                select = o;
                select.gameObject.SetActive(true);
                break;
            }
        }
            
        // 모두 사용하고 있으면
        if (!select)
        {
            // 새롭게 생성하고 select 변수에 할당
            select = Instantiate(_foodObject[index], transform);
            pools[index].Add(select);
        }
        
        return select;
    }
    
    
    private void SetSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
}
