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
    public Holder _liquidVolume;
    [SerializeField] float speed;
    [SerializeField]float value = .731f;
    [SerializeField] bool displayAmountUI = true;
    float counter = 0;
    float curreentliquidAmount;
    bool called;
    protected override void Start()
    {
        base.Start();
        calledSound = false;
    }
    IEnumerator ParticleEffect()
    {
        liquidParticle.gameObject.SetActive(true);

        yield return new WaitForSeconds(.1f);
        liquidParticle.gameObject.SetActive(false);
        yield return new WaitForSeconds(.1f);

        liquidParticle.gameObject.SetActive(true);

    }
    bool calledSound;
    protected virtual void Update()
    {
        if(grabed)
        {
            if (Physics.Raycast(spellPoint.position, Vector3.down, out hit, 10, targetLayer))
            {
                if (hit.collider.TryGetComponent(out Holder holder))
                {
                    _liquidVolume = holder;

                    //_liquidVolume = hit.collider.GetComponent<Holder>();
                    if (!calledSound)
                    {
                        calledSound = true;
                        AudioManagerMain.instance.PlaySFX("PouringSmall");
                    }
                    if (!called)
                    {
                        // AudioManagerMain.instance.PlaySFX("pouringLiquid");

                        curreentliquidAmount = _liquidVolume.liquidVolume.level;
                        called = true;
                    }
                    liquidParticle.gameObject.SetActive(true);


                    _liquidVolume.IncreaseLiquid(.01f * Time.deltaTime * 25);

                    if (displayAmountUI)
                    {
                        SceneController.instance.fillLiquidStatic.SetAmount(itemName, liquidMLFullAmount);
                        // SceneController.instance.SetShakerLiquidAmount(itemName, liquidMLFullAmount, .1f);
                        if (counter <= (liquidMLFullAmount + 2))
                        {

                            //  liquidUI.SetFillAmount(counter, liquidMLFullAmount, liquidMLFullAmount);
                            SceneController.instance.fillLiquidStatic.SetFillAmount(counter, liquidMLFullAmount, liquidMLFullAmount);
                            counter += (_liquidVolume.liquidVolume.level - curreentliquidAmount) * (.731f * speed);
                        }

                        SceneController.instance.fillLiquidStatic.gameObject.SetActive(true);
                    }
                    if (_liquidVolume.liquidVolume.level >= value)
                    {
                        grabed = false;
                        liquidParticle.gameObject.SetActive(false);
                        SceneController.instance.InvokeCurrentStep();
                        // SceneController.instance.fillLiquidUI.gameObject.SetActive(false);
                        SceneController.instance.fillLiquidStatic.gameObject.SetActive(false);
                        GetComponent<BoxCollider>().enabled = false;
                        _liquidVolume.DeactivateOutline();
                    }
                }
                else
                {
                    AudioManagerMain.instance.StopSound("PouringSmall");
                    liquidParticle.gameObject.SetActive(false);
                    calledSound = false;
                }
            }
        }
        else
        {
            liquidParticle.gameObject.SetActive(false);
          //  AudioManagerMain.instance.StopSound("PouringSmall");
         //   calledSound = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end")
        {
            UnGrab();
        }
        if(!grabed)
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
