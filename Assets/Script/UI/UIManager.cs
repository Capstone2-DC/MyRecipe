using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static List<string> ingredientsList = new List<string>();
    public static string Categories;

    [SerializeField] GameObject IngredientScroll;
    [SerializeField] GameObject recipe;
    [SerializeField] GameObject ingredientNeeded;
    [SerializeField] GameObject caution;

    private string ingredients;

    public GameObject[] backgroundImages; // 백그라운드 이미지

  /*  private Color activeColor = Color.green; // 활성화된 버튼의 색깔
    private Color inactiveColor = Color.white; // 비활성화된 버튼의 색깔*/

    public void IngredientBtn(string ingredient)
    {
        //재료 겹칠경우 리스트에 포함되지 않게 필터링
        if (ingredientsList.Contains(ingredient + " ")) return;

        ingredientsList.Add(ingredient + " ");
    }
    public void ButtonController(string btn)
    {
        switch (btn)
        {
            case "재료추가":
                IngredientScroll.SetActive(true);
                break;
            case "총0개의재료추가하기":
                foreach (string ingredi in ingredientsList)
                {
                    ingredients += ingredi;
                }
                //UpdateUI.Instance.UpdateText(ingredients);
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
        foreach (GameObject backgroundImage in backgroundImages)
        {
            //backgroundImage.color = inactiveColor;
            backgroundImage.SetActive(false);
        }
        // 선택한 버튼에 해당하는 백그라운드 이미지의 색상을 활성화 색깔로 변경
        //backgroundImages[btnIndex].color = activeColor;
        backgroundImages[btnIndex].SetActive(true);
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
