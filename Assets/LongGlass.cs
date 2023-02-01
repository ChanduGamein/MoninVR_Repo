using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LongGlass : HolderGlass
{
    public List<GameObject> iceCubes = new List<GameObject>();
    [SerializeField] GameObject dummySpoon;
    [SerializeField] GameObject spoon;
    [SerializeField] List<Transform> freshMint = new List<Transform>();
    [SerializeField] List<Transform> limeWedges = new List<Transform>();
    public bool mojito;
    [SerializeField]bool mint=true;
    public override void SetGarnishTransform(Transform garnish)
    {
        if (mojito)
        {
            if(mint)
            {
                SetfreshMint(garnish);
            }
            else
            {
                SetWidges(garnish);

            }

        }
        else
        {
            base.SetGarnishTransform(garnish);
        }
    }

    public void SetfreshMint(Transform garnish)
    {
        if (freshMint.Count > 0)
        {
            garnish.parent = transform;
            garnish.position = freshMint[0].position;
            garnish.rotation = freshMint[0].rotation;
            garnish.localScale = freshMint[0].localScale;
            freshMint.RemoveAt(0);
            if(freshMint.Count<=0)
            {
                Debug.Log("invoke Mint");
                SceneController.instance.InvokeCurrentStep();
                mint = false;
                hand.triggerGarnish = false;
            }
        }
    }
    public void SetWidges(Transform garnish)
    {
        if (limeWedges.Count > 0)
        {
            garnish.parent = transform;
            garnish.position = limeWedges[0].position;
            garnish.rotation = limeWedges[0].rotation;
            garnish.localScale = limeWedges[0].localScale;
            limeWedges.RemoveAt(0);
            if(limeWedges.Count<=0)
            {
                Debug.Log("invoke Lime");

                SceneController.instance.InvokeCurrentStep();
                mojito=false ;
                hand.triggerGarnish = false;
            }
        }
    }
    public void Stir()
    {
        dummySpoon.SetActive(true);
        dummySpoon.transform.DOLocalRotate(new Vector3(0,180,0),.2f).SetLoops(11).SetEase(Ease.Linear).OnComplete(()=> ActvateSpoon());
    }

    public void ActvateSpoon()
    {
        spoon.SetActive(true);
        dummySpoon.SetActive(false);
        SceneController.instance.InvokeCurrentStep();
    }
    public override void Grab()
    {
        base.Grab();
        if (!picked)
        {
            SceneController.instance.InvokeCurrentStep();
            picked = true;
        }
    }
    bool called;
    private void OnTriggerEnter(Collider other)
    {
        if (!grabed)
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.shakerPositon, hand, this.transform, "Shaker");
                //   UIManager.instance.grabButton.SetActive(true);
                UIManager.instance.canGrab = true;
            }
        if (other.tag == "end")
        {
            UnGrab();
        }
        if (other.tag == "Ice")
        {
            // shaker.hand.GetComponent<XRController>().SendHapticImpulse(.5f,.5f);
            if (iceCubes.Count > 0)
            {
                iceCubes[0].SetActive(true);
                iceCubes.RemoveAt(0);
                other.gameObject.SetActive(false);
                if (!called)
                {
                    called = true;
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

        }
    }
}
