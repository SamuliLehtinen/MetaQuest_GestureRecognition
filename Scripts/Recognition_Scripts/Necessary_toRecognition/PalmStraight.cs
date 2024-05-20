using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to detect when a hand is in a palm straight state
/// </summary>
public class PalmStraight : MonoBehaviour
{
    private OVRHand rightHand;

    [SerializeField]
    public GameObject palmCube;
    [SerializeField]
    public Material Material1;
    [SerializeField]
    public Material Material2;


    void Awake()
    {
        rightHand = GetComponent<OVRHand>();
    }
    
    Vector3 vecDifference = new Vector3(0.0f, 0.0f, 0.0f);

    float fDifference = 0.0f;
    float zDifference = 0.0f;

    
    public bool processPalm(OVRBone indexTip, OVRBone middleTip, OVRBone ringTip, OVRBone pinkyTip, OVRBone handBase, OVRBone thumbTip, OVRBone indexKnuckle)
    {

        Transform wristBase = handBase.Transform;
        
        bool isThumbStraight = false;
        bool isIndexStraight = false;
        bool isMiddleStraight = false;
        bool isRingStraight = false;
        bool isPinkyStraight = false;

        bool isHandOpened = false;
        // Detection of the max distance between the finger tips and wrist base
        if (Vector3.Distance(thumbTip.Transform.position, wristBase.position) > 0.12f)
        {
            isThumbStraight = true;
        }
        else
        {
            isThumbStraight = false;
        }

        if (Vector3.Distance(indexTip.Transform.position, handBase.Transform.position) > 0.17f)
        {
            isIndexStraight = true;
        }
        else
        {
            isIndexStraight = false;
        }

        if (Vector3.Distance(middleTip.Transform.position, handBase.Transform.position) > 0.18f)
        {
            isMiddleStraight = true;
        }
        else
        {
            isMiddleStraight = false;
        }

        if (Vector3.Distance(ringTip.Transform.position, handBase.Transform.position) > 0.17f)
        {
            isRingStraight = true;
        }
        else
        {
            isRingStraight = false;
        }

        if (Vector3.Distance(pinkyTip.Transform.position, handBase.Transform.position) > 0.145f)
        {
            isPinkyStraight = true;
        }
        else
        {
            isPinkyStraight = false;
        }

        if (isThumbStraight && isIndexStraight && isMiddleStraight && isRingStraight && isPinkyStraight)
        {
            isHandOpened = true;
        }
        else
        {
            isHandOpened = false;
        }

        //space between the fingers
        //gap 1 -> thumb and index
        //gap 2 -> index and middle
        //gap 3 -> middle and ring
        //gap 4 -> ring and pinky
        bool gap1 = false;
        bool gap2 = false;
        bool gap3 = false;
        bool gap4 = false;
        bool areFingerClosed = false;

        if(Vector3.Distance(thumbTip.Transform.position, indexKnuckle.Transform.position) < 0.045f)
        {
            gap1 = true;
        }
        else
        {
            gap1 = false;
        }

        if(Vector3.Distance(indexTip.Transform.position, middleTip.Transform.position) < 0.11f)
        {
            gap2 = true;
        }
        else
        {
            gap2 = false;
        }
        
        if(Vector3.Distance(middleTip.Transform.position, ringTip.Transform.position) < 0.026f)
        {
            gap3 = true;
        }
        else
        {
            gap3 = false;
        }
        
        if(Vector3.Distance(ringTip.Transform.position, pinkyTip.Transform.position) < 0.037f)
        {
            gap4 = true;
        }
        else
        {
            gap4 = false;
        }

        // condition with gap 1 makes the straight palm detection unnatural and more difficult
        //if(gap1 && gap2 && gap3 && gap4)
        if(gap2 && gap3 && gap4)
        {
            areFingerClosed = true;
        }
        else
        {
            areFingerClosed = false;
        }

        if(isHandOpened && areFingerClosed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
