using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static List<string> list = new List<string>();
    static Dictionary<string, int> dict = new Dictionary<string, int>();

    public static void Main()
    {
     /*   list.Add("계란");
        list.Add("참기름");
        list.Add("간장");
        list.Add("대패");
        list.Add("고추장");
        list.Add("파스타");
        list.Add("올리브유");*/

        RecipeDB.Loading();
        dict = RecipeDB.Find(list);
        dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        foreach(int i in dict.Values)
        {
            Debug.Log(i);
        }
        
       KeyValuePair<string, int> firstElement  = dict.First();
        string key = firstElement.Key;
        int value = firstElement.Value;
        UpdateUI.Instance.UpdateRecipe(key);
        Debug.Log(key);
    }
}
