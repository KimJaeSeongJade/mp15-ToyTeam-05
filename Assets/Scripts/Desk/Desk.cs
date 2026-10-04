using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour, IInteractable
{
    [SerializeField] private Food _currentFood;
    public Food CurrentFood => _currentFood;
    
    [SerializeField] private Transform _spawnPoint;
    private BoxCollider _boxCollider;


    private void Awake()
    {
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }
    
    // -------------------- 테스트
    [SerializeField] private Food _testHoldFood;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_testHoldFood == null)
            {
                Debug.Log("테스트 Food가 없습니다.");
                return;
            }

            Debug.Log("=== Desk Interact 테스트 ===");
            Interact(null, _testHoldFood);
        }
    }
    // -------------------- 테스트
    
    private void OnTriggerStay(Collider collision)
    {
        if (_currentFood != null) return;
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            _currentFood = collision.gameObject.GetComponent<Food>();
            
            _currentFood.transform.position = _spawnPoint.position;
            _currentFood.transform.rotation = _spawnPoint.rotation;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (_currentFood == null) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            _currentFood = null;
        }
    }
    

    public void Interact(IInteractor interactor)
    {
        return;
    }


    public void Interact(IInteractor interactor, IHoldable holdable)
    {
        Debug.Log("Interact 호출");
        if (_currentFood == null)
        {
            Debug.Log("Desk 위에 음식이 없습니다.");
            return;
        }
        else
        {
            Debug.Log($"Desk 음식 ID : {_currentFood.FoodId}");
            Debug.Log($"들고 있는 음식 ID : {holdable.FoodData.FoodId}");
            
            List<string> list = new List<string>();
            list.Add(_currentFood.FoodId);
            list.Add(holdable.FoodData.FoodId);

            Food resultfood = RecipeManager.Instance.GetRecipe(list);

            if (resultfood == null) return;
            
            Destroy(_currentFood.gameObject);
            GameObject foodObject = Instantiate(resultfood.gameObject, _spawnPoint.position, Quaternion.identity);
            _currentFood = foodObject.gameObject.GetComponent<Food>();
        }
    }
}
