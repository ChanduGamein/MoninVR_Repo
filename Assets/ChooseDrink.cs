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
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnClickDrink()
    {
        if (toggle.isOn)
        {
            Debug.Log("onn");
            UIManager.instance.ActivatePrepareBtn();
            UIManager.instance.SetInGameRecipeImage(drinkSprite,drinkName);
            SceneController.instance.ChooseRecipe(id);
        }
        else
        {
            UIManager.instance.DeActivatePrepareBtn();
        }
    }


}
