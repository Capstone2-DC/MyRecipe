using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    public static UpdateUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] TMP_Text haveIngredientText;
    [SerializeField] TMP_Text recipe;

    public void UpdateText(string text) 
    {
        haveIngredientText.text += " "+ text;
    }
    public void UpdateRecipe(string text)
    {
        recipe.text = text;
    }
}
