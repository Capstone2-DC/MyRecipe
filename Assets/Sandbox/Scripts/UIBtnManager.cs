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

    public void RecipeObjReset()
    {
        
    }
}
