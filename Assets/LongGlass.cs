using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LongGlass : HolderGlass
{
    public List<GameObject> iceCubes = new List<GameObject>();
    [SerializeField] GameObject dummySpoon;
    [SerializeField] GameObject spoon;
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
