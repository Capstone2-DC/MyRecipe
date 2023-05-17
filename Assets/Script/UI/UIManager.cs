using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static string Ingredients;
    public static string Categories;

    [SerializeField] GameObject IngredientScroll;

    [SerializeField] GameObject recipe;
    [SerializeField] GameObject ingredientNeeded;
    [SerializeField] GameObject caution;

    private string tempIngredient;
    

    public Image[] backgroundImages; // 백그라운드 이미지

    private Color activeColor = Color.green; // 활성화된 버튼의 색깔
    private Color inactiveColor = Color.white; // 비활성화된 버튼의 색깔

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
                Ingredients = tempIngredient;
                UpdateUI.Instance.UpdateText(Ingredients);
                IngredientScroll.SetActive(false);
                break;  
        }
    }
    public void CategoryBtn(string btn)
    {
        Categories = btn;
    }

    public void CategoryBtnColor(int btnIndex)
    {
        foreach (Image backgroundImage in backgroundImages)
        {
            backgroundImage.color = inactiveColor;
        }
        // 선택한 버튼에 해당하는 백그라운드 이미지의 색상을 활성화 색깔로 변경
        backgroundImages[btnIndex].color = activeColor;
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
