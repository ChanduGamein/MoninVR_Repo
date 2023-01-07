using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarnishItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask layer;
    RaycastHit hit;
    Rigidbody _rb;
    public HandHolder hand;
    [SerializeField] items itemType;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, -transform.up, Color.green);
        //if (Physics.Raycast(transform.position, -transform.up, out hit, 20, layer))
        //{
        //    Debug.Log("hiiiit   " + hit.transform.gameObject.name);
        //    Debug.Log("hit");
        //    transform.parent = hit.transform;
        //    hand.handCollider.enabled = true;
        //    hand.hasGarnish = false;
        //    gameObject.SetActive(false);
        //    SceneController.instance.glassDrink.garnish.SetActive(true);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cup")
        {
            GlassDrink glass = other.GetComponent<GlassDrink>();
            int rnd = Random.Range(0, glass.garnishPositions.Count);
            transform.parent = glass.transform;
            transform.position = glass.garnishPositions[rnd].position;
            transform.rotation = glass.garnishPositions[rnd].rotation;
            transform.localScale = glass.garnishPositions[rnd].localScale;
            hand.handCollider.enabled = true;
            hand.hasGarnish = false;
            for (int i = 0; i < SceneController.instance.currentRecipe.RecipeItems.Count; i++)
            {

                if (SceneController.instance.currentRecipe.RecipeItems[i].itemType == itemType)
                {
                    SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired -= 1;
                    if (SceneController.instance.currentRecipe.RecipeItems[i].numberOfItemsRequired <= 0)
                    {
                        SceneController.instance.InvokeCurrentStep();

                        //  UIManager.instance.pumpButton.SetActive(false);


                        //   Invoke(nameof(ReturnObjectToOriginalTransform),.5f);
                    }
                }
            }
        }
    }
    public void LeaveHAnd()
    {
       // _rb.isKinematic = false;

    }
}
