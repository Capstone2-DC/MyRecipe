using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Linq;
using static Unity.VisualScripting.Icons;
using static UnityEditor.Progress;
using System.IO;
using System.Text;

public static class Util
{
    #region
    /*함수 이름 짓는법
    Ex)
    OnInitialize() 클래스 초기화
    OnClick() 버튼 클릭


    1.대부분 동사 먼저 Ex) GetRevenue() < ReveneGet()
    2.필드는 소문자 시작, 프로퍼티, 메서드, 클래스 등등(대부분)은 대문자 시작,
    3.인터페이스는 맨 앞에 I붙이기. Ex) IButton
    4.필요 없는 단어 스킵 Ex) ShopManager.LoadShopData() -> ShopManager.LoadData(), ShopManager에서 shop의 데이터라는것을 유추 가능. 필요없는 단어 스킵
    5.줄임말 사용 금지. 단, 거의 표준어로 굳어졌다고 생각될 만큼이면 괜춘. Ex) Ads를 Advertisements...쓰긴 좀 그러니까 Ads로 사용, Korean을 Kor 정도 이 정도는 용인.
    6.Manager라는 포괄정인 단어보단 구체적인 단어 선택. 
    7.특수문자 사용 X Add_Revneue 언더바 정도는 자주 사용한다고 함.(취향차이인데 통일하는게 좋다)
    8.3단어 안넘는게 베스트긴 한데 진짜 맥시멈 5단어..
    9.짝으로 이름짓기 get이 있으면 set이 있을 것이라 예상가능 


    Coordinator 조정자
    Builder 무언갈 만드는. 
    Writer 값을 쓰는것만 가능한류 ?
    Reader 값을 쓸 일이 없는경우 오직 읽기 Ex) Rom
    Handler 다루다?-> 자동차의 핸들같은 존재
    Container 
    Protocol 규약
    Target 
    Converter 
    Controller
    View 
    Factory
    Entity 실체 객체
    Bucket
    
    ..구글링 요망
     */
    #endregion

    //클래스 생성시
    /*
    1.상속이 필요 없는 클래스들 Ex) Manager Class류들 생성시 sealed 붙이기.
     */


    #region 주석모음
    //"<color=#0000ff>" +  + "</color>"
    //.name.Substring(gameObject.name.IndexOf("_") + 1)
    //Image.sprite = Resources.Load("Link", typeof(Sprite)) as Sprite;
    //int.Parse
    //GameObject a = Instantiate(CopyObject, new Vector2(0, 0), Quaternion.identity, Mom.transform);
    // foreach (KeyValuePair<,> _ in _)
    //System.Enum.GetValues(Type).Length
    /*
 for(int i =0; i< Dic.Count; i++)
{
    if(Dic.Keys.ToList()[i] == name)
    {
        Dic[name] = bCheck;
    }
    else if(Dic.Values.ToList()[i] == true)
    {
        Dic[name] = bCheck;
    }
} 
     
    Coordinator 조정자
    Builder 
    Writer
    Reader
    Handler 다루다?-> 자동차의 핸들같은 존재
    Container 
    Protocol 규약
    Target 
    Converter 
    Controller
    View 
    Factory
    Entity 실체 객체
    Bucket
     */

    #endregion


    public static void Log<T>(string text, T num)//편한 로그.
    {
        Debug.Log(text + " is " + num);
    }
    public static int SumArray(int[] array)//배열의 총합값
    {
        int n = 0;
        for (int i = 0; i < array.Length; i++)
            n += array[i];
        return n;
    }
    public static float SumArray(float[] array)//배열의 총합값
    {
        float n = 0;
        for (int i = 0; i < array.Length; i++)
            n += array[i];
        return n;
    }
    public static string FormatComma(long number)
    {
        return string.Format("{0:#,##0}", number);
    }
    public static string FormatComma(int number)
    {
        return string.Format("{0:#,##0}", number);
    }
    public static string FormatComma(double value, int decimalPlaces)
    {
        return string.Format("{0:F" + decimalPlaces + "}", value);
    }

    public static string FormatComma(float value, int decimalPlaces)
    {
        return string.Format("{0:F" + decimalPlaces + "}", value);
    }
    public static void InitArray<T>(int[] array, int resetnumber)
    {
        for (int i = 0; i < array.Length; i++)
            array[i] = resetnumber;
    }
    public static float Round(float num, int n)//n번째 숫자까지 올림.
    {
        return (float)Math.Round(num, n);
    }
    public static float Round(double num, int n)//n번째 숫자까지 올림.
    {
        return (float)Math.Round(num, n);
    }
    public static bool Probability(double chance)
    {
        if (chance <= 0 || chance >= 1)
        {
            throw new ArgumentException("Chance should be between 0 and 1.");
        }
        return (UnityEngine.Random.value <= chance);
    }
    public static bool Probability(float chance)
    {
        if (chance <= 0 || chance >= 1)
        {
            throw new ArgumentException("Chance should be between 0 and 1.");
        }
        return (UnityEngine.Random.value <= chance);
    }
    public static int ParamsProb(params float[] pers)//확률을 차례대로(합이 1이되는것을 권장) 넣었을 때 해당하는 확률이 걸리면 01234가 차례로 나옴.
    {
        int maxLenth = 0;

        if (pers.Length == 1)//배열길이가 1이면 무조건 그 값이 나와야함으로 0을 리턴한다.
            return 0;

        //1.가장 소숫점이 긴 변수를 찾아낸다.
        int lenth = 0;
        decimal total = 0;
        for (int i = 0; i < pers.Length; i++)
        {
            lenth = pers[i].ToString().Substring(pers[i].ToString().IndexOf('.') + 1).Length;

            total += (decimal)pers[i];
            if (maxLenth < lenth)
                maxLenth = lenth;
        }

        int correction = (int)(total * (decimal)Math.Pow(10, maxLenth)); //-> 곱해주는 수 

        int randomNumber = UnityEngine.Random.Range(1, correction + 1);
        int tempNum = 0;
        int num = 0;
        for (int i = 0; i < pers.Length; i++)
        {
            num = (int)(correction * (decimal)pers[i]);
            if (num + tempNum >= randomNumber)
            {
                //Debug.Log(num + tempNum + ">=" + randomNumber);
                return i;
            }
            tempNum += num;
        }
        Debug.Log(total + "-" + maxLenth + "-" + randomNumber + "-" + tempNum + "-" + num);
        Debug.Log("ERROR : 정상적이지 않은 접근." + correction);
        return 0;
    }
    public static int GetRandomIndex(params double[] probabilities)
    {
        double totalProbability = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            if (probabilities[i] < 0 || probabilities[i] > 1)
            {
                throw new ArgumentOutOfRangeException($"Probability at index {i} is out of range (0~1)");
            }

            totalProbability += probabilities[i];
        }

        if (totalProbability < 0 || totalProbability > 1)
        {
            throw new ArgumentException("Sum of probabilities is out of range (0~1)");
        }

        double randomValue = new Random().NextDouble();

        double probabilitySum = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilitySum += probabilities[i];

            if (randomValue < probabilitySum)
            {
                return i;
            }
        }
        return probabilities.Length - 1;
    }

    public static string Pertext(float num)
    {
        return Math.Round((num) * 100, 2) + "%";
    }
    public static string Pertext(double num)
    {
        return Math.Round((num) * 100, 2) + "%";
    }
    public static string Pertext(int num)
    {
        return num.ToString();
    }
    public static T RandomArray<T>(T[] per)
    {
        int n = UnityEngine.Random.Range(0, per.Length);
        return per[n];
    }
    public static T RandomParamsArray<T>(params T[] per)
    {
        int n = UnityEngine.Random.Range(0, per.Length);
        return per[n];
    }
    public static T RandomList<T>(List<T> per)
    {
        int n = UnityEngine.Random.Range(0, per.Count);
        return per[n];
    }

    public static string TimeFormat(double timeValue)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeValue);
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        int seconds = timeSpan.Seconds;

        if (timeValue < 60) // 60초 미만
        {
            return seconds.ToString("00");
        }
        else if (timeValue < 3600) // 1시간 미만
        {
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else // 1시간 이상
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }

    #region Max~Min
    public static int Max(int num, int max)
    {
        if (num > max)
            return max;
        return num;
    }
    public static float Max(float num, float max)
    {
        if (num > max)
            return max;
        return num;
    }
    public static long Max(long num, long max)
    {
        if (num > max)
            return max;
        return num;
    }
    public static int Min(int num, int min)
    {
        if (num < min)
            return min;
        return num;
    }
    public static float Min(float num, float min)
    {
        if (num < min)
            return min;
        return num;
    }


    public static long Min(long num, long min)
    {
        if (num < min)
            return min;
        return num;
    }
    public static int MinMax(int num, int min, int max)
    {
        if (num < min)
            return min;
        if (num > max)
            return max;
        return num;
    }
    public static float MinMax(float num, float min, float max)
    {
        if (num < min)
            return min;
        if (num > max)
            return max;
        return num;
    }
    public static long MinMax(long num, long min, long max)
    {
        if (num < min)
            return min;
        if (num > max)
            return max;
        return num;
    }
    public static bool SequenceEqual<T>(List<T> list1, List<T> list2)
    {
        return Enumerable.SequenceEqual(
                   list1.OrderBy(a => a), list2.OrderBy(a => a));
    }

    public static long ExtraExcelConvert(string text)
    //엑셀에서 자릿수 높은 숫자 로드시 사용
    {
        if (text.Contains('.'))
        {
            Debug.Log("Input number is not decimal. Input :" + text);
            return -1;
        }

        if (text.Contains("E"))
            return (long)(Double.Parse(text.Substring(0, text.IndexOf("+") - 1)) * Math.Pow(10, Int64.Parse(text.Substring(text.IndexOf("+") + 1))));
        return Int64.Parse(text);
    }
    public static List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }
    public static T[] Shuffle<T>(T[] list)
    {
        for (int i = list.Length - 1; i > 0; i--)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }

    public static string PrintArray<T>(T[] arrays)
    {
        StringBuilder stringReader = new StringBuilder();
        foreach (var item in arrays)
        {
            stringReader.Append(item.ToString() + ", ");
        }
        return stringReader.ToString();
    }

    public static int MostFrequent(int[] arr)
    {
        // Sort the array
        Array.Sort(arr);

        // find the max frequency using
        // linear traversal
        int max_count = 1, res = arr[0];
        int curr_count = 1;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == arr[i - 1])
                curr_count++;
            else
                curr_count = 1;

            // If last element is most frequent
            if (curr_count > max_count)
            {
                max_count = curr_count;
                res = arr[i - 1];
            }
        }

        return res;
    }
    #endregion

}
