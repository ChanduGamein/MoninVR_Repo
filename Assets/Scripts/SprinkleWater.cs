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
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform spellPoint;
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public string itemName;

    RaycastHit hit;
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
    private void Update()
    {
        Debug.DrawRay(spellPoint.position, Vector3.down, Color.green);
        if(grabed)
        {

            if (Physics.Raycast(spellPoint.position,Vector3.down,out hit,20,targetLayer))
            {
                Debug.Log(hit.transform.gameObject.name);
                SceneController.instance.SetGlassLiquidAmount(itemName,liquidMLFullAmount,liquidMLPerPump);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (!poured)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform);
                UIManager.instance.canGrab = true;
            }
        if (other.gameObject.tag == "Cup")
        {
            // UIManager.instance.pourSparklinButton.SetActive(true);
           // PourIntoGlass();
            //  glassDrink = other.GetComponent<GlassDrink>();
            //   shaker = other.transform;
        }
    }
    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false ;

            // UIManager.instance.grabButton.SetActive(false);
        }
    }
}
