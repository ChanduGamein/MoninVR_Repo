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
    [SerializeField] HolderGlass glassDrink;
    RaycastHit hit;
    public List<GameObject> iceCubes = new List<GameObject>();
    [SerializeField] GameObject StrainerDummy;
    [SerializeField] float amountToAdd;
    [SerializeField] float shakeSpeed=3.5f;
    float Counter;
    bool calledSound;
    bool calledIce = false;
    public void AddStrainer()
    {
        StrainerDummy.SetActive(true);
        SceneController.instance.InvokeCurrentStep();
    }
    public bool shaking;
    private void OnEnable()
    {
        SceneController.instance._shaker = this;
    }
    public override void Grab()
    {
        base.Grab();
        if(callTutoral)
        if(!picked)
        {
            SceneController.instance.InvokeCurrentStep();
            picked = true;
        }
        if(lidPlaced)
        {
            hand.GetComponent<ShakeDetector>().SetShaker(this);

            hand.GetComponent<ShakeDetector>().Detectshake = true;

        }
    }
    public void SetCalled()
    {
     //   called = false;
    }
    public override void UnGrab()
    {
        if (hand != null)
        {
            hand.GetComponent<ShakeDetector>().Detectshake = false;
            hand.GetComponent<ShakeDetector>().inity = 0;

        }
        if(!shaking)
        base.UnGrab();
    }
    public void SetPourToGlass()
    {
        PourToGlass = true;
    }
    bool lidPlaced = false;
    public void Shake()
    {
        dummyLid.SetActive(true);
        lidPlaced = true;
        hand.GetComponent<ShakeDetector>().SetShaker(this);

        hand.GetComponent<ShakeDetector>().Detectshake = true;
    }
    public void ShakerAnimation()
    {
        lidPlaced = false;

        shaking = true;

        transform.DOShakePosition(shakeSpeed, 10).OnComplete(() => FinishShake());

    }
    public void FinishShake()
    {
       dummyLid.SetActive(false);
        shaking = false;
        AudioManagerMain.instance.StopSound("shakerSound");
        AudioManagerMain.instance.PlaySFX("shakerMixerOpen");

        SceneController.instance.InvokeCurrentStep();
        shakerLid.SetActive(true);
        UnGrab();
        hand.animator.SetTrigger("Idle");
    }


    private void Update()
    {
        if (PourToGlass)
            if (grabed)
            {
                Debug.DrawRay(pourPosition.position, Vector3.down, Color.green);
                if (Physics.Raycast(pourPosition.position, Vector3.down, out hit, 10, targetLayer))
                {
                    if (liquidVolume.level > 0)
                    {
                        if(!calledSound)
                        {
                            calledSound = true;
                            AudioManagerMain.instance.PlaySFX("PouringSmall");
                        }
                        glassDrink.IncreaseLiquid(amountToAdd * Time.deltaTime*5);
                        liquidVolume.level -= .1f * Time.deltaTime*5;
                        liquidParticle.gameObject.SetActive(true);

                    }
                    else
                    {

                        PourToGlass = false;
                        liquidParticle.gameObject.SetActive(false);
                        glassDrink.DeactivateOutline();
                        AudioManagerMain.instance.StopSound("PouringSmall");
                        calledSound = false;
                        SceneController.instance.InvokeCurrentStep();
                    }
                }
                else
                {
                    liquidParticle.gameObject.SetActive(false);
                    AudioManagerMain.instance.StopSound("PouringSmall");
                    calledSound = false;

                }
            }
            else
            {
                liquidParticle.gameObject.SetActive(false);
                AudioManagerMain.instance.StopSound("PouringSmall");
                calledSound = false;

            }

    }
    public void ActivateIce()
    {
        if (iceCubes.Count >= 4)
            for (int i = 0; i < 4; i++)
            {
                // shaker.hand.GetComponent<XRController>().SendHapticImpulse(.5f, .5f);
                iceCubes[0].SetActive(true);
                iceCubes.RemoveAt(0);
                if (!calledIce)
                {
                    calledIce = true;
                    SceneController.instance.InvokeCurrentStep();
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
                if(hand.handType==HandType.right)
                {
                    liquidParticle.transform.localPosition = poringRight.localPosition;
                }
                else
                {
                   liquidParticle.transform.localPosition = poringLeft.localPosition;

                }
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (other.tag == "Ice")
        {
            // shaker.hand.GetComponent<XRController>().SendHapticImpulse(.5f,.5f);
            AudioManagerMain.instance.PlaySFX("IceIntoGlass");

            if (iceCubes.Count > 0)
            {
                iceCubes[0].SetActive(true);
                iceCubes.RemoveAt(0);
                other.gameObject.SetActive(false);
                if(!calledIce)
                {
                    calledIce = true;
                    SceneController.instance.InvokeCurrentStep();
                }
            }
            else
            {
                other.gameObject.SetActive(false);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
         //   UIManager.instance.grabButton.SetActive(false);
            UIManager.instance.canGrab = false;
        //    UIManager.instance.ExitTrigger();
        }
    }


}
