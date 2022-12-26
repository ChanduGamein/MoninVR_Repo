using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SprinkleWater : Holder
{
    [SerializeField]Transform shaker;
    [SerializeField]GlassDrink glassDrink;
    [SerializeField] Transform shakerPourPosition;
    [SerializeField] int amountToPour;
    bool poured;
    public void PourIntoGlass()
    {
        poured = true;
        _rb.isKinematic = true;
        shaker.GetComponent<Rigidbody>().isKinematic = true;

        UIManager.instance.pourSparklinButton.SetActive(false);
        transform.DOMove(shakerPourPosition.position, 1);
        transform.DORotate(shakerPourPosition.rotation.eulerAngles, 1);
        StartCoroutine(LiquidCounter());
    }
    IEnumerator LiquidCounter()
    {
        int counter = 0;
        yield return new WaitForSeconds(1);
        while (counter<amountToPour)
        {
            counter += 1;
            glassDrink.amountTxt.text = counter.ToString();
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(1);
        glassDrink.amountTxt.gameObject.SetActive(false);
        SceneController.instance.InvokeCurrentStep();
        UnGrab();
        shaker.GetComponent<Rigidbody>().isKinematic = false;

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
                //UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform);
            }
        if (other.gameObject.tag == "Cup")
        {
            // UIManager.instance.pourSparklinButton.SetActive(true);
            PourIntoGlass();
            //  glassDrink = other.GetComponent<GlassDrink>();
            //   shaker = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
           // UIManager.instance.grabButton.SetActive(false);
        }
    }
}
