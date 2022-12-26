using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class GlassDrink : Holder
{
    public TextMeshProUGUI amountTxt;
    [SerializeField] Transform shakerPourPosition;
    [SerializeField]Transform shaker;
    bool poured;
    public void PourIntoGlass()
    {
        poured = true;

        UIManager.instance.pourButton.SetActive(false);
        shaker.GetComponent<Rigidbody>().isKinematic = true;

        shaker.DOMove(shakerPourPosition.position,1);
        shaker.DORotate(shakerPourPosition.rotation.eulerAngles,1);
        StartCoroutine(RetrunShaker());
    }
    IEnumerator RetrunShaker()
    {
        yield return new WaitForSeconds(2);
        SceneController.instance.InvokeCurrentStep();
        shaker.GetComponent<Shaker>().UnGrab();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!poured)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                //hand = other.GetComponent<HandHolder>();

                //UIManager.instance.ActivateGrab(hand.shakerPositon, hand,this.transform);
            }
        if (other.gameObject.tag == "Shaker")
        {
            PourIntoGlass();
            //  UIManager.instance.pourButton.SetActive(true);
            shaker = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
        //    UIManager.instance.grabButton.SetActive(false);
        }

    }
}
