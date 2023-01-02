using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XrGrabInteractableAttachTransform : XRGrabInteractable
{
    public Transform leftAttachPoint;
    public Transform rightAttachPoint;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("Lhand"))
        {
            attachTransform = leftAttachPoint;
        }
        if(args.interactorObject.transform.CompareTag("Rhand"))
        {
            attachTransform = rightAttachPoint;
        }
        base.OnSelectEntered(args);
    }
}
