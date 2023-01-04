using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillLiquidUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text currentAmount;
    [SerializeField] Text fullAmount;
    [SerializeField] int totalAmount;
    [SerializeField] Text drinkName;
    [SerializeField] Image fillImage;
    float calculatedAmount = 0;
    public void SetAmount(string _drinkName,int _fullAmount)
    {
        drinkName.text = _drinkName;
        fullAmount.text = _fullAmount.ToString()+"ml";
        totalAmount = _fullAmount;
    }
    public void InCreaseAmount( float amountTtAdd = 3)
    {
        if (calculatedAmount < totalAmount)
        {
            calculatedAmount += amountTtAdd;
            fillImage.fillAmount = (calculatedAmount / (float)totalAmount);
            currentAmount.text = calculatedAmount.ToString();
        }
        else
        {
          //  SceneController.instance.ResetShakerLiquidUI();

        }
        
    }
    public void ResetValues()
    {
        calculatedAmount = 0;
        fillImage.fillAmount = 0;
    }
    public void Test()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
