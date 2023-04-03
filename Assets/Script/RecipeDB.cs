using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class RecipeDB
{
    static List<Recipe> recipeList = new List<Recipe>();

    public static void Loading()
    {
        recipeList = LoadRecipe("Recipe");
    }
    public static Dictionary<string, int> Find(List<string> haveIngredient)
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();

        foreach (Recipe recipe in recipeList)
        {    
            foreach(string ingredient in recipe.IngredientList)
            {
                foreach(string have in haveIngredient)
                {
                    if(ingredient == have)
                    {          
                        if (!dict.ContainsKey(recipe.Name))
                        {
                            dict.Add(recipe.Name, 0);
                        }
                        dict[recipe.Name]++;
                        continue;
                    }
                }
            }
        }
        /*  foreach (int i in dict.Values)
          {
              Debug.Log(i);
          }
          foreach (string s in dict.Keys)
          {
              Debug.Log(s);
          }*/
        foreach (Recipe recipe1 in recipeList)
        {
            Debug.Log(recipe1.Name);
            foreach (string lists in recipe1.IngredientList)
            {
                Debug.Log(lists);
            }
        }
        return dict;
    }

    public static List<Recipe> LoadRecipe(string name)
    {
        List<Recipe> recipe = new List<Recipe>();
        List<List<string>> List = DataLoader.Load("DB/" + name);

       // List<string> ingredientList = new List<string>(); 
        for (int i = 0; i < List.Count; i++)
        { 
            List<string> ingredientList = new List<string>();
            string ID = List[i][0];
            // float slowresist = float.Parse(List[i][4]);
            // long cost = Util.ExtraExcelConvert(List[i][7]);
            // int Arti = int.Parse(List[i][8]);

            for (int j = 1; j<= 4; j++) //ID 인덱스 0 제외한 1번부터, 재료 개수만큼
            {
                ingredientList.Add(List[i][j]);     
            }  
            recipe.Add(new Recipe(ID, ingredientList));
            //ingredientList.Clear(); -> 이렇게 하면 왜 안되지?

        }
        return recipe;
    }
}


/*   public static void Loading()
      {
          characterList = LoadCharacter("Character");
      }*/

/*    public static Character Copy(string ID)
    {
        return Find(ID).CopyCharacter();
    }
    public static Character Copy(int index)
    {
        return Find(index).CopyCharacter();
    }*/



/*  public static Character Find(string ID)
  {

      foreach (Character character in characterList)
      {
          if (character.ID.Equals(ID))
              return character;
      }
      Debug.Log("Error_Unknown character......");
      return null;
  }*/
/*   public static Character Find(int index)
   {
       if (index < 0 || index > characterList.Count - 1)
           return null;
       return characterList[index];
   }*/


/*   public Dictionary<string, int> Find(List<string> haveIngredient) 
   {
       List<List<string>> foodDataBase = DataLoader.Load("Data/Character/" + "Recipe");
       Dictionary<string, int> dict = new Dictionary<string, int>();

       foreach(List<string> foodData in foodDataBase)
       {
           foreach(string ingredient in foodData)
           {
               foreach(string have in haveIngredient)
               {
                   if (ingredient == have)
                   {
                       foodDataBase[ID].
                   }
               }
           }
       }
   }*/
