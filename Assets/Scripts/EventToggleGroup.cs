using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventToggleGroup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject mocktailPanel, LemonadePanel, iceTeaPanel;
    [SerializeField] Toggle mocktailBtn,lemonadeBtn,iceteaBtn,WhiteTeaBTn,mojitoBtn,fizzyLemonadeBtn;
    [SerializeField] ToggleGroup[] drinksToggleGroup;
    public void ResetDrinksToggleToOff()
    {
        for (int i = 0; i < drinksToggleGroup.Length; i++)
        {
                drinksToggleGroup[i].SetAllTogglesOff();
        }
        UIManager.instance.DeActivatePrepareBtn();
    }
    public void OnValueChangedMockTail()
    {
        ResetDrinksToggleToOff();

        if (mocktailBtn.isOn)
        {
            mocktailPanel.SetActive(true);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);


        }
        else
        {

            mocktailPanel.SetActive(false);
        }
    }
    public void OnValueChangedLemonde()
    {
        ResetDrinksToggleToOff();

        if (lemonadeBtn.isOn)
        {
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(true);
            iceTeaPanel.SetActive(false);

        }
        else
        {

            LemonadePanel.SetActive(false);
        }

    }
    public void OnValueChangedIceTea()
    {
        ResetDrinksToggleToOff();

        if (iceteaBtn.isOn)
        {
            iceTeaPanel.SetActive(true);
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);


        }
        else
        {

            iceTeaPanel.SetActive(false);
        }
    }
    public void OnValueChangedIceTeaWhite()
    {
        ResetDrinksToggleToOff();

        if (WhiteTeaBTn.isOn)
        {
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);

        }
        else
        {

        }
    }
    public void OnValueChangedMojito()
    {
        ResetDrinksToggleToOff();

        if (mojitoBtn.isOn)
        {
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);


        }
        else
        {

        }
    }
    public void OnValueChangedFizzyLemonade()
    {
        ResetDrinksToggleToOff();

        if (fizzyLemonadeBtn.isOn)
        {
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);


        }
        else
        {

        }
    }

}
