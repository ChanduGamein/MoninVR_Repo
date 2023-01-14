using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;
using LiquidVolumeFX;
public class Shaker : Holder
{
    public GameObject dummyLid;
    public GameObject shakerLid;
    public bool PourToGlass;
    public Transform pourPosition;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GlassDrink glassDrink;
    RaycastHit hit;
    public List<GameObject> iceCubes = new List<GameObject>();
    public void SetCounter(float _value)
    {
        value = _value;
    }

    public void SetPourToGlass()
    {
        PourToGlass = true;
    }
    public void Shake()
    {
        dummyLid.SetActive(true);
        transform.DOShakePosition(9.5f, 1.2f).OnComplete(() => FinishShake());
    }
    public void FinishShake()
    {
       dummyLid.SetActive(false);

      //  AudioManagerMain.instance.StopSound("shakerMixerOpen");
        AudioManagerMain.instance.PlaySFX("shakerMixerOpen");
        //  transform.parent = transform.parent.parent;
        // shaker.GetComponent<Rigidbody>().isKinematic = false;
        SceneController.instance.InvokeCurrentStep();
        shakerLid.SetActive(true);

    }
    float Counter;
    float value;
    private void Update()
    {
        if(PourToGlass)
        if (grabed)
        {
                Debug.DrawRay(pourPosition.position, Vector3.down,Color.green);
            if (Physics.Raycast(pourPosition.position, Vector3.down, out hit, 10, targetLayer))
            {
                    if (Counter < value)
                    {
                        glassDrink.IncreaseLiquid(.01f);
                        Counter += .01f;
                        DecreaseLiquid(.1f);
                    }
                    else
                    {
                        PourToGlass = false;
                        SceneController.instance.InvokeCurrentStep();
                    }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform,"Shaker");
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
