using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shaker : Holder
{
    public GameObject dummyLid;
    public GameObject shakerLid;
    public bool PourToGlass;
    public Transform pourPosition;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GlassDrink glassDrink;
    RaycastHit hit;
    public void SetPourToGlass()
    {
        PourToGlass = true;
    }
    public void Shake()
    {
        dummyLid.SetActive(true);
        transform.DOShakePosition(9.5f, 1).OnComplete(() => FinishShake());
    }
    public void FinishShake()
    {
       dummyLid.SetActive(false);


        AudioManagerMain.instance.PlaySFX("shakerMixerOpen");
        //  transform.parent = transform.parent.parent;
        // shaker.GetComponent<Rigidbody>().isKinematic = false;
        SceneController.instance.InvokeCurrentStep();
        shakerLid.SetActive(true);
        shakerLid.SetActive(true);

    }
    private void Update()
    {
        if(PourToGlass)
        if (grabed)
        {

            if (Physics.Raycast(pourPosition.position, Vector3.down, out hit, 20, targetLayer))
            {
                    glassDrink.IncreseLiquidGradually(.3f);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform);
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
         //   UIManager.instance.grabButton.SetActive(false);
            UIManager.instance.canGrab = false;

        }
    }


}
