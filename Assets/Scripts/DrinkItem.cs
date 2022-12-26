using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public enum objectType
{
    ShakerObject, IceCubeBox,bottle,ShakerLid,IceScoop
}
public class DrinkItem : MonoBehaviour , IPointerClickHandler
{
    public items itemType;
    public objectType objectType;
    public Transform targetPosition;
    public Transform targetPosition2;
    Vector3 originalPosition;
    Quaternion originalRotation;
    bool targetReached;
    [SerializeField] int currentAddedAmount=0;
    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (objectType)
        {
            case objectType.ShakerObject:
                Debug.Log("shaker");
                break;
            case objectType.bottle:
                Debug.Log("base");
                if (!targetReached)
                {
                    transform.DOMove(targetPosition.position, 1);
                    transform.DORotate(targetPosition.rotation.eulerAngles, 1);
                    targetReached = true;
                }
                else
                {
                 // StartCoroutine(  PumpAnimation());
                }
                break;
            case objectType.ShakerLid:
                Debug.Log("lid");
                if (!targetReached)
                {
                    transform.DOMove(targetPosition.position, 1);
                    transform.DORotate(targetPosition.rotation.eulerAngles, 1);
                    targetReached = true;
                }
                else
                {
                 //   Shake();
                }
                break;
            case objectType.IceScoop:
                Debug.Log("scoop");
                if (!targetReached)
                {
                    transform.DOMove(targetPosition.position, 1);
                   //  =>  PickUpIce());
                }
                else
                {
                    transform.DOMove(targetPosition2.position, 1);
                 //   transform.DORotate(targetPosition2.rotation.eulerAngles, 1).OnComplete(()=> ActivatePhysicsOnCubes());
                }
                //else
                //{
                //    Shake();
                //}
                break;

            default:
                break;
        }
        // throw new System.NotImplementedException();
    }

    



    public void ReturnObjectToOriginalTransform()
    {
        GetComponent<Collider>().enabled = false;
        SceneController.instance.currentAddedAmount = 0;
        transform.DOMove(originalPosition, 1);
        transform.DORotate(originalRotation.eulerAngles, 1);
    }
}
