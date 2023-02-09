using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CahngeHeightButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float height;
    [SerializeField] Transform xrOrigin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rhand" || other.gameObject.tag == "Lhand")
        {
            //   UIManager.instance.grabButton.SetActive(false);
            xrOrigin.position = new Vector3(xrOrigin.position.x,xrOrigin.position.y+height,xrOrigin.position.z);
        }
    }
}
