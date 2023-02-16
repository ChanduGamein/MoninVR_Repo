using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fridge : MonoBehaviour
{
    [SerializeField]Transform glassDoor;
    [SerializeField] Outline outline;
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
            {
            glassDoor.DOLocalRotate(new Vector3(0, -120, 0), 2f);
            SetFridgeOpen(true);
            outline.enabled = false;
            }



    }
    public void SetFridgeOpen(bool isOpen)
    {
        SceneController.instance.isFridgeOpen = isOpen;
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            glassDoor.DOLocalRotate(new Vector3(0, 0, 0), 2f).OnComplete(() => SetFridgeOpen(false));

        }
    }
}
