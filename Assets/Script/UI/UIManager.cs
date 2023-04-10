using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static string ingredients;

    [SerializeField] GameObject IngredientScroll;

    [SerializeField] GameObject recipe;
    [SerializeField] GameObject ingredientNeeded;
    [SerializeField] GameObject caution;

    private string tempIngredient;

    public void IngredientBtn(string ingredient)
    {
        /*  Manager.list.Add(ingredient);
          UpdateUI.Instance.UpdateText(ingredient);*/
        tempIngredient += ingredient + " ";
        //활성화된 재료를 다시 선택할 경우 tempIngredient에서 다시선택된 ingredient 뺴야함. 그럼 string형식이아니라 list로 해야할수도
    }

    public void ButtonController(string btn)
    {
        switch (btn)
        {
            case "재료추가":
                IngredientScroll.SetActive(true);
                break;
            case "총0개의재료추가하기":
                ingredients = tempIngredient;
                UpdateUI.Instance.UpdateText(ingredients);
                IngredientScroll.SetActive(false);
                break;  
        }
    }

    public void ChatGptContents(string btn)
    {
        recipe.SetActive(false);
        ingredientNeeded.SetActive(false);
        caution.SetActive(false);
        switch (btn)
        {
            case "recipe":
                recipe.SetActive(true);
                break;
            case "ingredientNeeded":
                ingredientNeeded.SetActive(true);
                break;
            case "caution":
                caution.SetActive(true);
                break;
        }
    }
}
