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
        //Ȱ��ȭ�� ��Ḧ �ٽ� ������ ��� tempIngredient���� �ٽü��õ� ingredient ������. �׷� string�����̾ƴ϶� list�� �ؾ��Ҽ���
    }

    public void ButtonController(string btn)
    {
        switch (btn)
        {
            case "����߰�":
                IngredientScroll.SetActive(true);
                break;
            case "��0��������߰��ϱ�":
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
