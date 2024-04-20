using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems.OVRInputModule;


public class PalmStraight : MonoBehaviour
{
    private OVRHand rightHand;
    //private HandFinger rightHandFinger;
    private bool isFistClosed = false;

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
        //Debug.Log("PA Distance thumb tip : " + Vector3.Distance(thumbTip.Transform.position, wristBase.position));
        if (Vector3.Distance(thumbTip.Transform.position, wristBase.position) > 0.12f)
        {
            isThumbStraight = true;
        }
        else
        {
            isThumbStraight = false;
        }

        //Debug.Log("PA Distance index tip : " + Vector3.Distance(indexTip.Transform.position, wristBase.position));

        if (Vector3.Distance(indexTip.Transform.position, handBase.Transform.position) > 0.17f)
        {
            isIndexStraight = true;
        }
        else
        {
            isIndexStraight = false;
        }

        //Debug.Log("PA Distance middle tip : " + Vector3.Distance(middleTip.Transform.position, wristBase.position));

        if (Vector3.Distance(middleTip.Transform.position, handBase.Transform.position) > 0.18f)
        {
            isMiddleStraight = true;
        }
        else
        {
            isMiddleStraight = false;
        }

        //Debug.Log("PA Distance ring tip : " + Vector3.Distance(ringTip.Transform.position, wristBase.position));

        if (Vector3.Distance(ringTip.Transform.position, handBase.Transform.position) > 0.17f)
        {
            isRingStraight = true;
        }
        else
        {
            isRingStraight = false;
        }

        //Debug.Log("PA Distance pinky tip : " + Vector3.Distance(pinkyTip.Transform.position, wristBase.position));

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

        //Debug.Log("PA Distance gap1 : " + Vector3.Distance(thumbTip.Transform.position, indexKnuckle.Transform.position));
        if(Vector3.Distance(thumbTip.Transform.position, indexKnuckle.Transform.position) < 0.045f)
        {
            gap1 = true;
        }
        else
        {
            gap1 = false;
        }

        //Debug.Log("PA Distance gap2 : " + Vector3.Distance(indexTip.Transform.position, indexKnuckle.Transform.position));

        if(Vector3.Distance(indexTip.Transform.position, middleTip.Transform.position) < 0.11f)
        {
            gap2 = true;
        }
        else
        {
            gap2 = false;
        }
        
        //Debug.Log("PA Distance gap3 : " + Vector3.Distance(middleTip.Transform.position, indexTip.Transform.position));

        if(Vector3.Distance(middleTip.Transform.position, ringTip.Transform.position) < 0.026f)
        {
            gap3 = true;
        }
        else
        {
            gap3 = false;
        }
        
        //Debug.Log("PA Distance gap4 : " + Vector3.Distance(ringTip.Transform.position, pinkyTip.Transform.position));

        if(Vector3.Distance(ringTip.Transform.position, pinkyTip.Transform.position) < 0.037f)
        {
            gap4 = true;
        }
        else
        {
            gap4 = false;
        }

        if(gap1 && gap2 && gap3 && gap4)
        {
            areFingerClosed = true;
        }
        else
        {
            areFingerClosed = false;
        }

        Debug.Log("PA isHandOpened : " + isHandOpened);
        Debug.Log("PA areFingerClosed : " + areFingerClosed);

        if(isHandOpened && areFingerClosed)
        {
            //palmCube.GetComponent<Renderer>().material = Material1;
            //Debug.Log("PA Palm : is straight");
            return true;
        }
        else
        {
            //palmCube.GetComponent<Renderer>().material = Material2;
            //Debug.Log("PA Palm : is NOT straight");

            return false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        // Check if the hand is in the fist closed state
        isFistClosed = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }
}
