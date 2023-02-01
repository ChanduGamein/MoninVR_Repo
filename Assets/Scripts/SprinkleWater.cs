using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using LiquidVolumeFX;
public class SprinkleWater : Holder
{

    [SerializeField] Transform spellPoint;
    public int liquidMLPerPump;
    public int liquidMLFullAmount;
    public string itemName;
    [SerializeField] Image fillImage;
    [SerializeField]LayerMask targetLayer;
    RaycastHit hit;
  //  [SerializeField] FillLiquidUI liquidUI;
    [SerializeField] Holder _liquidVolume;
    [SerializeField] float speed;


    float counter = 0;
    float curreentliquidAmount;
    bool called;
    IEnumerator ParticleEffect()
    {
        liquidParticle.gameObject.SetActive(true);

        yield return new WaitForSeconds(.1f);
        liquidParticle.gameObject.SetActive(false);
        yield return new WaitForSeconds(.1f);

        liquidParticle.gameObject.SetActive(true);

    }
    protected virtual void Update()
    {
        if(grabed)
        {
            Debug.DrawRay(spellPoint.position,Vector3.down,Color.green);
            if (Physics.Raycast(spellPoint.position,Vector3.down,out hit,20,targetLayer))
            {
                _liquidVolume = hit.collider.GetComponent<Holder>();
                if (!called)
                {
                    curreentliquidAmount = _liquidVolume.liquidVolume.level;
                    called = true;
                }
                liquidParticle.gameObject.SetActive(true);

              //  StartCoroutine(ParticleEffect());

                Debug.Log(hit.transform.gameObject.name);
                //   glassDrink.IncreseLiquidGradually(1);
                _liquidVolume.IncreaseLiquid(.01f * Time.deltaTime *10);


              //  liquidUI.gameObject.SetActive(true);
               // liquidUI.SetAmount(itemName,liquidMLFullAmount);
                SceneController.instance.fillLiquidStatic.SetAmount(itemName,liquidMLFullAmount);
                // SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, .1f);
                if(counter<=liquidMLFullAmount)
                {

                  //  liquidUI.SetFillAmount(counter, liquidMLFullAmount, liquidMLFullAmount);
                    SceneController.instance.fillLiquidStatic.SetFillAmount(counter, liquidMLFullAmount, liquidMLFullAmount);
                    counter+= (_liquidVolume.liquidVolume.level-curreentliquidAmount)*(.731f*speed);
                }
                 //liquidUI.SetFillAmount(_liquidVolume.liquidVolume.level, .731f,liquidMLFullAmount);
                 //SceneController.instance.fillLiquidStatic.SetFillAmount(_liquidVolume.liquidVolume.level, .731f,liquidMLFullAmount);
                
                SceneController.instance.fillLiquidStatic.gameObject.SetActive(true) ;
                if (_liquidVolume.liquidVolume.level>=.731f)
                {
                    grabed = false;
                    liquidParticle.gameObject.SetActive(false);
                    SceneController.instance.InvokeCurrentStep();
                   // SceneController.instance.fillLiquidUI.gameObject.SetActive(false);
                    SceneController.instance.fillLiquidStatic.gameObject.SetActive(false);

                }
            }
            else
            {
                liquidParticle.gameObject.SetActive(false);
            }
        }
        else
        {
            liquidParticle.gameObject.SetActive(false);

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
                UIManager.instance.ActivateGrab(hand.waterBottlePosition, hand, this.transform, "Shaker");
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

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            UIManager.instance.canGrab = false ;

            // UIManager.instance.grabButton.SetActive(false);
        }
    }
}
