using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ButtonController()
    {
        
    }

    public void IngredientBtn(string ingredient)
    {
        Manager.list.Add(ingredient);
        UpdateUI.Instance.UpdateText(ingredient);
    }
}