using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe 
{
   public string Name { get; private set; }
   public List<string> IngredientList = new List<string>();

   public Recipe(string name, List<string> ingredientList)
    {
        this.Name = name;
        this.IngredientList = ingredientList;
    }
}
