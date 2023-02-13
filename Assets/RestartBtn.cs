using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBtn : MonoBehaviour
{
    [SerializeField] bool handOnItem;

    public void OnClick()
    {
        if (handOnItem)
        {
            SceneController.instance.Reload();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            handOnItem = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            handOnItem = false;


        }
    }
}
