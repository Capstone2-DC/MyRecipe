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
    

    public Image[] backgroundImages; // ��׶��� �̹���

    private Color activeColor = Color.green; // Ȱ��ȭ�� ��ư�� ����
    private Color inactiveColor = Color.white; // ��Ȱ��ȭ�� ��ư�� ����

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
        // ������ ��ư�� �ش��ϴ� ��׶��� �̹����� ������ Ȱ��ȭ ����� ����
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
