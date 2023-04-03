using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPrefab : MonoBehaviour
{
    Button btn;
    private void OnEnable()
    {
        btn = GetComponent<Button>();
    }
}
