using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventToggleGroup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject mocktailPanel, LemonadePanel, iceTeaPanel,whiteTeaPanel,mojitoPanel,fizzyLemonadePanel;
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
            whiteTeaPanel.SetActive(false);
            mojitoPanel.SetActive(false);
            fizzyLemonadePanel.SetActive(false);

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
            whiteTeaPanel.SetActive(false);
            mojitoPanel.SetActive(false);
            fizzyLemonadePanel.SetActive(false);
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
            whiteTeaPanel.SetActive(false);
            mojitoPanel.SetActive(false);
            fizzyLemonadePanel.SetActive(false);

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
            whiteTeaPanel.SetActive(true);
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);
            mojitoPanel.SetActive(false);
            fizzyLemonadePanel.SetActive(false);

        }
        else
        {

            whiteTeaPanel.SetActive(false);
        }
    }
    public void OnValueChangedMojito()
    {
        ResetDrinksToggleToOff();

        if (mojitoBtn.isOn)
        {
            whiteTeaPanel.SetActive(false);
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);
            mojitoPanel.SetActive(true);
            fizzyLemonadePanel.SetActive(false);

        }
        else
        {

            mojitoPanel.SetActive(false);
        }
    }
    public void OnValueChangedFizzyLemonade()
    {
        ResetDrinksToggleToOff();

        if (fizzyLemonadeBtn.isOn)
        {
            whiteTeaPanel.SetActive(false);
            mocktailPanel.SetActive(false);
            LemonadePanel.SetActive(false);
            iceTeaPanel.SetActive(false);
            mojitoPanel.SetActive(false);
            fizzyLemonadePanel.SetActive(true);

        }
        else
        {

            mojitoPanel.SetActive(false);
        }
    }

}
