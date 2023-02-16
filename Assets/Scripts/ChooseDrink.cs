using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseDrink : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int id = 0;
    [SerializeField]Toggle toggle;
    [SerializeField] Sprite drinkSprite;
    [SerializeField] string drinkName;
    [SerializeField] Image drinkImage;
    [SerializeField] Text drinkTxt;
    void Start()
    {
        toggle = GetComponent<Toggle>();
        drinkImage.sprite = drinkSprite;
     //   drinkTxt.text = drinkName;
    }

    public void OnClickDrink()
    {
        if (toggle.isOn)
        {
            Debug.Log("onn");
            UIManager.instance.ActivatePrepareBtn();
            UIManager.instance.SetInGameRecipeImage(drinkSprite,drinkName);
            UIManager.instance.drinkId = id;
            //  SceneController.instance.ChooseRecipe(id);
            AudioManagerMain.instance.PlaySFX("Click");
        }
        else
        {
            UIManager.instance.DeActivatePrepareBtn();
        }
    }


}
