using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance { get; private set; }

    [SerializeField] private List<Recipe> _recipes;

    private void Awake() => SetSingleton();
    
    // 음식 조합할때 RecipeManager.Instance.GetRecipe() 로 불러야 하는데
    // 매개변수로 string 타입 리스트를 줘야함
    // 예를 들어        식탁 02 : 채썬 양배추,
    // 손에 들고 있는 음식   04 : 채썬 토마토
    // [02] [04] 이렇게 넘겨야함
    // 상호 작용 시 넘길때 List 만들어서
    // 테이블에 있는 음식 id, 손에 들고 있는 id 둘 다 Add 해서 만든 리스트로 불러야함
    public Food GetRecipe(List<string> foodIds)
    {
        string recipeId = CreateRecipeId(foodIds);

        foreach (Recipe recipe in _recipes)
        {
            if (recipe.ResultFood.FoodId == recipeId)
            {
                return recipe.ResultFood;
            }
        }

        return null;
    }

    private string CreateRecipeId(List<string> foodIds)
    {
        List<int> foodids = new List<int>();

        // 지금은 괜찮은데 만약 음식 3개 조합 시에 0204(토마토 샐러드) + 12(???) 이런식은 작동 x
        // 레시피 3개 만들때는 수정해야함
        foreach (string foodId in foodIds)
        {
            foodids.Add(int.Parse(foodId));
        }

        foodids.Sort();

        string recipeId = "";

        foreach (int foodid in foodids)
        {
            recipeId += foodid.ToString("D2");
        }

        return recipeId;
    }
    
    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
