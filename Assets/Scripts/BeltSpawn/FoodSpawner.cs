using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _topRight;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _bottomRight;

    [SerializeField] private int _spawntimefood;
    [SerializeField] private List<FoodSpawnData> _foodSpawnData;
    Queue<FoodSpawnData> _spawnData = new();

    // --------------- 오브젝트풀
    [SerializeField] private PoolManager _poolManager;

    [SerializeField] private int num;
    
    // --------------- 오브젝트풀
    
    
    [SerializeField] private SpecialOrder _specialOrder;
    
    private bool _changeSpawn = true;

    private void Start()
    {
        foreach (FoodSpawnData fooddata in _foodSpawnData)
        {
            StartCoroutine(CooldownRoutine(fooddata));
        }
        StartCoroutine(SpawnFood());
    }



    // ----------------------- 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_poolManager.Get(num);
        }
    }
    // ----------------------- 테스트
    
    
    
    
    
    
    
    

    private IEnumerator CooldownRoutine(FoodSpawnData fooddata)
    {
        while (true)
        {
            yield return new WaitForSeconds(fooddata.Cooldown);

            _spawnData.Enqueue(fooddata);
        }
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            // 경쟁 모드 일때 같은 음식 나오도록
            if (_gameData.GameMode == 0)
            {
                yield return new WaitForSeconds(_spawntimefood);

                if (_spawnData.Count < 2) continue;

                FoodSpawnData firstfood = _spawnData.Dequeue();
                //FoodSpawnData sceondfood = _spawnData.Dequeue();

                if (_changeSpawn)
                {
                    if (_specialOrder._beltStop1 == false)
                    {
                        SpawnFood(firstfood, _topLeft);    
                    }

                    if (_specialOrder._beltStop2 == false)
                    {
                        SpawnFood(firstfood, _bottomRight);
                    }
                }
                else
                {
                    if (_specialOrder._beltStop1 == false)
                    {
                        SpawnFood(firstfood, _bottomLeft);
                    }

                    if (_specialOrder._beltStop2 == false)
                    {
                        SpawnFood(firstfood, _topRight);
                    }
                }


                _changeSpawn = !_changeSpawn;
            }
            // 협동 모드일때 다른 음식 나오도록
            else
            {
                yield return new WaitForSeconds(_spawntimefood);

                if (_spawnData.Count < 2) continue;

                FoodSpawnData firstfood = _spawnData.Dequeue();
                FoodSpawnData sceondfood = _spawnData.Dequeue();

                if (_changeSpawn)
                {
                    if (_specialOrder._beltStop1 == false)
                    {
                        SpawnFood(firstfood, _topLeft);    
                    }

                    if (_specialOrder._beltStop2 == false)
                    {
                        SpawnFood(sceondfood, _bottomRight);
                    }
                }
                else
                {
                    if (_specialOrder._beltStop1 == false)
                    {
                        SpawnFood(firstfood, _bottomLeft);
                    }

                    if (_specialOrder._beltStop2 == false)
                    {
                        SpawnFood(sceondfood, _topRight);
                    }
                }


                _changeSpawn = !_changeSpawn;
            }
            
        }
    }

    private void SpawnFood(FoodSpawnData food, Transform spawnpoint)
    {
        //SoundManager.Instance.SFXPlay(SFXType.Belt);
        food.Food.CanHolding = true;
        float randomY = Random.Range(0f, 360f);
        
        Quaternion randomRotation = Quaternion.Euler(spawnpoint.rotation.eulerAngles.x, randomY, spawnpoint.rotation.eulerAngles.z);
        
        //Instantiate(food.Food, spawnpoint.position, randomRotation);
        
        Food ob = _poolManager.Get(food.Food);
        
        ob.transform.position = spawnpoint.position;
        ob.transform.rotation = randomRotation;
    }


}
