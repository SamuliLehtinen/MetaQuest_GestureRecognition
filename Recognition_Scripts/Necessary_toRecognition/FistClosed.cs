using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FistClosed : MonoBehaviour
{
    // This script is used to detect when a hand is in a fist closed state
    private OVRHand rightHand;
    private bool isFistClosed = false;

    [SerializeField]
    public GameObject fistCube;
    [SerializeField]
    public Material Material1;
    [SerializeField]
    public Material Material2;

    Vector3 vecDifference = new Vector3(0.0f, 0.0f, 0.0f);

    float fDifference = 0.0f;
    float zDifference = 0.0f;

    // This function detects a closed fist by calculating the distance between the tip of the hand fingers and the wrist
    public bool processFist(OVRBone indexTip, OVRBone middleTip, OVRBone ringTip, OVRBone pinkyTip, OVRBone handBase, OVRBone thumbTip, OVRBone middleMiddleKnuckle)
    {
        bool isIndexTipClosed = false;
        bool isMiddleTipClosed = false;
        bool isRingTipClosed = false;
        bool isPinkyTipClosed = false;

        //Check if the index tip is closed
        if (Vector3.Distance(indexTip.Transform.position, middleTip.Transform.position) < 0.1f)
        {
            isIndexTipClosed = true;
        }
        else
        {
            isIndexTipClosed = false;
        }
        //Check if the middle tip is closed
        if (Vector3.Distance(middleTip.Transform.position, ringTip.Transform.position) < 0.1f)
        {
            isMiddleTipClosed = true;
        }
        else
        {
            isMiddleTipClosed = false;
        }
        //Check if the ring tip is closed
        if (Vector3.Distance(ringTip.Transform.position, pinkyTip.Transform.position) < 0.1f)
        {
            isRingTipClosed = true;
        }
        else
        {
            isRingTipClosed = false;    
        }
        //Check if the pinky tip is closed
        if (Vector3.Distance(pinkyTip.Transform.position, handBase.Transform.position) < 0.1f)
        {
            isPinkyTipClosed = true;
        }
        else
        {
            isPinkyTipClosed = false;
        }

        //Check if the thumb tip is closed
        bool isThumbTipClosed = false;
        fDifference = Vector3.Distance(thumbTip.Transform.position, middleMiddleKnuckle.Transform.position);
        if(fDifference < 0.03f)
        {
            isThumbTipClosed = true; 
        }
        else
        {
            isThumbTipClosed = false;
        }


        //final condition check
        if(isIndexTipClosed && isMiddleTipClosed && isRingTipClosed && isPinkyTipClosed && isThumbTipClosed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
