using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using LiquidVolumeFX;
public class SprinkleWater : Holder
{
    [SerializeField]GlassDrink glassDrink;
    [SerializeField]Shaker shakerVolume;
    [SerializeField]LongGlass longGlassVolume;
    public LayerMask targetLayerCup;
    public LayerMask targetLayerShaker;
    public LayerMask targetLayerLongGlass;
    [SerializeField] Transform spellPoint;
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public string itemName;
    [SerializeField] Image fillImage;
    [SerializeField]LayerMask targetLayer;
    RaycastHit hit;
    [SerializeField] FillLiquidUI liquidUI;
    [SerializeField] ParticleSystem liquidParticle;
    Holder _liquidVolume;
    public void SetTargetLayer(LayerMask layerMask)
    {
        targetLayer = layerMask;
    }
    public void SetTargetLayerToCup()
    {
        targetLayer = targetLayerCup;
        _liquidVolume = glassDrink;
    }
    public void SetTargetLayerToShaker()
    {
        targetLayer = targetLayerShaker;
        _liquidVolume = shakerVolume;
    }
    public void SetTargetLayerToLongGlass()
    {
        targetLayer = targetLayerLongGlass;
        _liquidVolume = longGlassVolume;
    }

    private void Update()
    {
        if(grabed)
        {
            Debug.DrawRay(spellPoint.position,Vector3.down,Color.green);
            if (Physics.Raycast(spellPoint.position,Vector3.down,out hit,20,targetLayer))
            {
                liquidParticle.Play();
                Debug.Log(hit.transform.gameObject.name);
                //   glassDrink.IncreseLiquidGradually(1);
                _liquidVolume.IncreaseLiquid(.01f * Time.deltaTime * 10);


                liquidUI.gameObject.SetActive(true);
                liquidUI.SetAmount(itemName,liquidMLFullAmount);
                SceneController.instance.fillLiquidStatic.SetAmount(itemName,liquidMLFullAmount);
                // SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, .1f);
                liquidUI.SetFillAmount(_liquidVolume.liquidVolume.level,.731f);
                SceneController.instance.fillLiquidStatic.SetFillAmount(_liquidVolume.liquidVolume.level,.731f);
                SceneController.instance.fillLiquidStatic.gameObject.SetActive(true) ;
                if (fillImage.fillAmount>=1)
                {
                    grabed = false;
                    liquidParticle.Stop();
                    SceneController.instance.InvokeCurrentStep();
                    SceneController.instance.fillLiquidUI.gameObject.SetActive(false);
                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }

            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
                hand = other.GetComponent<HandHolder>();
                UIManager.instance.ActivateGrab(hand.smallBottlePosition, hand, this.transform, "SmallBottle");
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
