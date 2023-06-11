using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    private enum ActiveButtonType    // 안씀     판넬 활성화 되있는지 여부 확인
    {
        On, Off
    }

    public List<Transform> recipeObj;

    public void PanelActive(Transform movePanel)
    {
        if(movePanel.name != this.name)
        {
            movePanel.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }

   /*
    public void PanelActive(Transform falsePanel, Transform truePanel)
    {
        falsePanel.gameObject.SetActive(false);
        truePanel.gameObject.SetActive(true);
    }
    public void PanelActive(Transform PopUp, ActiveButtonType btnType)
    {
        if(btnType == ActiveButtonType.On)
        {
            PopUp.gameObject.SetActive(true);
        }
        else { PopUp.gameObject.SetActive(false); }
    }
   */
    public void PanelPopUp()
    {

    }


    public void LimitAddObjects(Transform item)
    {
        // RecordPanel에 사용되는 최근기록을 위한 함수, List에 5개의 레시피가 저장되있을경우 오래된 객체부터 삭제
        recipeObj.Add(item);
        if (recipeObj.Count > 5)
        {
            Destroy(recipeObj[0].gameObject);
            recipeObj.RemoveAt(0);
        }
            
    }
    
    public void DelObj(Transform item)
    {
        for(int i =0; i < recipeObj.Count; i++)
        {
            if(recipeObj[i] == item)
            {
                Destroy(recipeObj[i].gameObject);
                recipeObj.RemoveAt(i);
            }
        }
    }
    
}
