using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _topRight;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _bottomRight;

    [SerializeField] private int _spawntimefood;
    [SerializeField] private List<FoodSpawnData> _foodSpawnData;
    Queue<FoodSpawnData> _spawnData = new();

    private bool _changeSpawn = true;

    private void Start()
    {
        foreach (FoodSpawnData fooddata in _foodSpawnData)
        {
            StartCoroutine(CooldownRoutine(fooddata));
        }
        StartCoroutine(SpawnFood());
    }

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
            yield return new WaitForSeconds(_spawntimefood);

            if (_spawnData.Count < 2) continue;

            FoodSpawnData firstfood = _spawnData.Dequeue();
            FoodSpawnData sceondfood = _spawnData.Dequeue();

            if (_changeSpawn)
            {
                SpawnFood(firstfood, _topLeft);
                SpawnFood(sceondfood, _bottomRight);
            }
            else
            {
                SpawnFood(firstfood, _topRight);
                SpawnFood(sceondfood, _bottomLeft);
            }


            _changeSpawn = !_changeSpawn;
        }
    }

    private void SpawnFood(FoodSpawnData food, Transform spawnpoint)
    {
        float randomY = Random.Range(0f, 360f);
        
        Quaternion randomRotation = Quaternion.Euler(spawnpoint.rotation.eulerAngles.x, randomY, spawnpoint.rotation.eulerAngles.z);
        
        Instantiate(food.Food, spawnpoint.position, randomRotation);
    }


}
