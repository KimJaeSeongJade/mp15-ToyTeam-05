using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class Desk : MonoBehaviour, IInteractable, IHolder
{
    [SerializeField] private Food _currentFood;
    public Food CurrentFood => _currentFood;
    
    [SerializeField] private Transform _spawnPoint;
    private BoxCollider _boxCollider;

    public Transform TargetTransform => _spawnPoint;
    public bool IsHolding => _currentFood != null;
    public bool CanHold => false;
    public bool CanRelease => false;


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
            if(_currentFood.IsHolding) return;
            _currentFood.transform.position = _spawnPoint.position;
            _currentFood.transform.rotation = _spawnPoint.rotation;
            _currentFood.Hold(this, PLAYER_ID.NONE);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (_currentFood == null) return;
        if (collision.gameObject.GetComponent<Food>() == _currentFood)
        {
            _currentFood = null;
        }
    }

    public void RemoveData(Food food)
    {
        if (food == _currentFood)
            _currentFood = null;
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
            Debug.Log($"Desk 음식 ID : {_currentFood.FoodName}");
            Debug.Log($"들고 있는 음식 ID : {holdable.FoodData.FoodName}");
            
            List<string> list = new List<string>();
            list.Add(_currentFood.FoodId);
            list.Add(holdable.FoodData.FoodId);

            Food resultfood = RecipeManager.Instance.GetRecipe(list);

            if (resultfood == null) return;

            holdable.Release();

            // 손에 있는 음식 비활성화 
            holdable.FoodData.ReturnToPool();

            // Merge이후 주석처리 해제 예정
            //interactor.ReleaseItem();

            _currentFood.ReturnToPool();

            // 오브젝트풀로 변경
            // Destroy(_currentFood.gameObject);
            // GameObject foodObject = Instantiate(resultfood.gameObject, _spawnPoint.position, Quaternion.identity);

            Food ob = PoolManager.Instance.Get(resultfood);

            ob.transform.position = _spawnPoint.position;
            ob.transform.rotation = Quaternion.identity;

            _currentFood = ob.gameObject.GetComponent<Food>();
        }
    }
}
