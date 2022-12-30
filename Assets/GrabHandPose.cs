using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;
    // Start is called before the first frame update
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;
    private Quaternion[] startingFingerRotation;
    private Quaternion[] finalFingerRotation;
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetPose);
        grabInteractable.selectExited.AddListener(UnSetPose);
        rightHandPose.gameObject.SetActive(false);
    }
    public void SetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponent<HandData>();
            handData.animator.enabled = false;
            SetHandValues(handData, rightHandPose);
            SetHandData(handData,finalHandPosition,finalHandRotation,finalFingerRotation);
        }
    }
    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponent<HandData>();
            handData.animator.enabled = true;
            SetHandValues(handData, rightHandPose);
            SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotation);
        }
    }
    public void SetHandValues(HandData h1, HandData h2)
    {
        startingHandPosition = new Vector3(h1.root.localPosition.x/h1.root.localScale.x,
            h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);

        finalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z); 

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotation = new Quaternion[h1.fingerBones.Length];
        finalFingerRotation = new Quaternion[h1.fingerBones.Length];
        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotation[i] = h1.fingerBones[i].localRotation;
            finalFingerRotation[i] = h2.fingerBones[i].localRotation;
        }
    }
    public void SetHandData(HandData h,Vector3 newPosition, Quaternion newRotation,Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;
        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }
}
