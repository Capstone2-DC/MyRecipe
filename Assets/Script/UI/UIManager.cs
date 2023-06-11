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

    public GameObject[] backgroundImages; // ��׶��� �̹���

  /*  private Color activeColor = Color.green; // Ȱ��ȭ�� ��ư�� ����
    private Color inactiveColor = Color.white; // ��Ȱ��ȭ�� ��ư�� ����*/

    public void IngredientBtn(string ingredient)
    {
        //��� ��ĥ��� ����Ʈ�� ���Ե��� �ʰ� ���͸�
        if (ingredientsList.Contains(ingredient + " ")) return;

        ingredientsList.Add(ingredient + " ");
    }
    public void ButtonController(string btn)
    {
        switch (btn)
        {
            case "����߰�":
                IngredientScroll.SetActive(true);
                break;
            case "��0��������߰��ϱ�":
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
        // ������ ��ư�� �ش��ϴ� ��׶��� �̹����� ������ Ȱ��ȭ ����� ����
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
