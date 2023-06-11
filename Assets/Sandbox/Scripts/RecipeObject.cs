using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeObject : MonoBehaviour
{
    // 버튼 속성
    public Sprite image = null;
    public string name = "test";
    
    public bool isFavorite = false;

    private string recordPanelName = "RecordPanel";
    private string favoritePanelName = "FavoritePanel";

    [SerializeField]
    private Transform record = null;
    [SerializeField]
    private Transform favor = null;

    public void Start()
    {

        record = GameObject.Find("Canvas").transform.Find(recordPanelName);
        favor = GameObject.Find("Canvas").transform.Find(favoritePanelName);

        ImageBtnReset();
        RecipeNameReset(name);

        print(favor.Find("RecipeScrollView").Find("Viewport").Find("Content"));
    }

    public void RecipeNameReset(string name)
    {
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
    }



    public void FavoriteBtn()
    {
        // 즐겨찾는 메뉴 추가 버튼
        if (isFavorite) isFavorite = false;
        else isFavorite = true;
        ImageBtnReset();
        MoveObjForPanel();
    }
    private void ImageBtnReset()
    {
        // 즐겨찾기 버튼 이미지 새로고침
        if (isFavorite)
        {
            this.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
    }
    
    public void FavoriteBoxPopUp()
    {

    }


    public void MoveObjForPanel()
    {
        if(isFavorite)
        {
           
            var item = Instantiate(this.gameObject);
            item.transform.parent = favor.Find("RecipeScrollView").Find("Viewport").Find("Content");
            Destroy(this.gameObject);
        }
        else
        {
            
            var item = Instantiate(this.gameObject);
            item.transform.parent = record.Find("RecipeScrollView").Find("Viewport").Find("Content");
            Destroy(this.gameObject);
        }
    }
    
}
