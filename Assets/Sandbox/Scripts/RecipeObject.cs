using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeObject : MonoBehaviour
{
    // ��ư �Ӽ�
    public Sprite image = null;
    private string imageAreaName = "ImageBox";

    public string name = "test";
    private string nameAreaName = "RecipeTxt";
    
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
        RecipeObjRefresh();

        print(favor.Find("RecipeScrollView").Find("Viewport").Find("Content"));
    }

    
    public void RecipeObjRefresh()
    {
        if (image != null) this.transform.Find(imageAreaName).GetComponent<Image>().sprite = image;

        this.transform.Find(nameAreaName).GetComponent<TextMeshProUGUI>().text = name;

    }


    public void FavoriteBtn()
    {
        // ���ã�� �޴� �߰� ��ư
        if (isFavorite) isFavorite = false;
        else isFavorite = true;
        ImageBtnReset();
        MoveObjForPanel();
    }
    private void ImageBtnReset()
    {
        // ���ã�� ��ư �̹��� ���ΰ�ħ
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
   
    
    public void PopUpEvent()
    {
        // ������ ������Ʈ ������ �� �۵��Ǵ� �Լ�
    }


    public void MoveObjForPanel()
    {
        if(isFavorite)
        {
           
            var item = Instantiate(this.gameObject);
            record.GetComponent<UIBtnManager>().DelObj(this.transform);
            favor.GetComponent<UIBtnManager>().recipeObj.Add(item.transform);
            item.transform.parent = favor.Find("RecipeScrollView").Find("Viewport").Find("Content");
            Destroy(this.gameObject);
        }
        else
        {
            
            var item = Instantiate(this.gameObject);
            favor.GetComponent<UIBtnManager>().DelObj(this.transform);
            record.GetComponent<UIBtnManager>().LimitAddObjects(item.transform);
            item.transform.parent = record.Find("RecipeScrollView").Find("Viewport").Find("Content");
            Destroy(this.gameObject);
        }
    }
    
}
