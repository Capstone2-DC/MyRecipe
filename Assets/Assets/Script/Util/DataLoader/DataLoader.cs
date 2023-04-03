using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static List<List<string>> Load(string link)
    {
        var list = new List<List<string>>();
        string data_String = "";
        StringReader sr1 = null;
        StreamReader sr2 = null;
        if (Application.platform == RuntimePlatform.Android)
        {
            //"Data/Language.csv"
            TextAsset data = Resources.Load<TextAsset>(link);
            sr1 = new StringReader(data.text);
        }
        else
        {
            sr2 = new StreamReader(Application.dataPath + "/Resources/" + link + ".csv");
        }

        bool endOfFile = false;
        while (!endOfFile)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                data_String = sr1.ReadLine();
            }
            else
            {
                data_String = sr2.ReadLine();
            }
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_String.Split('@'); //string, string타입
            var tmp = new List<string>();
            for (int i = 0; i < data_values.Length; i++)
                tmp.Add(data_values[i]); //int, string으로 바뀜
            list.Add(tmp);
        }
        return list;
    }
}
