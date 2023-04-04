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
     /*   list.Add("���");
        list.Add("���⸧");
        list.Add("����");
        list.Add("����");
        list.Add("������");
        list.Add("�Ľ�Ÿ");
        list.Add("�ø�����");*/

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
