using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 


public class PoseDetectorSingle : MonoBehaviour
{

    // Define an enum for the direction
    public enum Handedness { Right, Left }

    public Handedness handType;

    [SerializeField]
    private OVRHand hand;

    [SerializeField]
    private OVRSkeleton handSkeleton;


    private FistClosed fistDetector;
    private PalmStraight palmDetector;
    private FistBump fistBumpDetector;


    private bool currentFist;
    private bool currentPalm;

    private bool lastFist;
    private bool lastPalm;

    public event Action FistClosedRight;
    public event Action FistNotClosedRight;
    public event Action PalmOpenedRight;
    public event Action PalmNotOpenedRight;

    public event Action FistClosedLeft;
    public event Action FistNotClosedLeft;
    public event Action PalmOpenedLeft;
    public event Action PalmNotOpenedLeft;

    //public event Action FistClosedRight, FistNotClosedRight, PalmOpenedRight, PalmNotOpenedRight;
    //public event Action FistClosedLeft, FistNotClosedLeft, PalmOpenedLeft, PalmNotOpenedLeft;



    OVRBone thumbTip;
    OVRBone indexTip;
    OVRBone middleTip;
    OVRBone ringTip;
    OVRBone pinkyTip;

    OVRBone handBase;
    OVRBone middleMiddleKnuckle;
    OVRBone indexMiddleKnuckle;



    void Awake()
    {
        fistDetector = GetComponent<FistClosed>();
        palmDetector = GetComponent<PalmStraight>();
        fistBumpDetector = GetComponent<FistBump>();
        if(!hand) hand = GetComponent<OVRHand>();
        if(!handSkeleton) 
        {
            handSkeleton = GetComponent<OVRSkeleton>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentFist = false;
        currentPalm = false;
        lastFist = false;
        lastPalm = false;        
    }

    // Update is called once per frame
    void Update()
    {

        if (hand.IsTracked)
        {
            //bones ID are the same for both hands = NO NEED FOR SWTICH HERE
            //switch (handType)
            //{
            //case Handedness.Right:
                // parameters for Right hand
                thumbTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MaxSkinnable);
                indexTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbMetacarpal);
                middleTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip);
                ringTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_RingTip);
                pinkyTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbTip);

                handBase = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Start);
                middleMiddleKnuckle = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_LeftArmUpper);

                indexMiddleKnuckle = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Head);
            //break;
            //case Handedness.Left:
                // Move left


            //break;
            //default:
            //break;
        }

        // Polling of known hand poses 
        currentFist = fistDetector.processFist(indexTip, middleTip, ringTip, pinkyTip, handBase, thumbTip, middleMiddleKnuckle);
        currentPalm = palmDetector.processPalm(indexTip, middleTip, ringTip, pinkyTip, handBase, thumbTip, indexMiddleKnuckle);
        // Debug.Log("PA PoseDetector current States : Fist = " + currentFist + " Palm = " + currentPalm);
        // Detection of a change in the fist gesture
        if (currentFist != lastFist)
        {
            //Debug.Log("PA PoseDetector condition : Fist State Changed");
            lastFist = currentFist;
            switch (handType)
            {
                case Handedness.Right:
                    //Debug.Log("PA PoseDetector RightFist : Current Fist = " + currentFist + " FistClosed event = " + FistClosedRight);

                    if (currentFist && FistClosedRight != null)
                    {
                        //Debug.Log("PA PoseDetector Script : Fist Event Invoked");
                        //invoke the event
                        FistClosedRight.Invoke();
                    }
                    else if (!currentFist && FistNotClosedRight != null)
                    {
                        //Debug.Log("PA PoseDetector Script : Fist Event Invoked");
                        //invoke the event
                        FistNotClosedRight.Invoke();
                    }
                break;
                case Handedness.Left:
                    //Debug.Log("PA PoseDetector LeftFist : Current Fist = " + currentFist + " FistClosedLeft event = " + FistClosedLeft);
                    if (currentFist && FistClosedLeft != null)
                    {
                        //Debug.Log("PA PoseDetector Left : Fist Event Invoked");
                        //invoke the event
                        FistClosedLeft.Invoke();
                    }
                    else if (!currentFist && FistNotClosedLeft != null)
                    {
                        //Debug.Log("PA PoseDetector Left : Fist Event Contrary Invoked");
                        //invoke the event
                        FistNotClosedLeft.Invoke();
                    }
                break;
                default:
                break;
                
            }
        }
        

        
        // Detection of a change in the palm gesture
        if (currentPalm != lastPalm)
        {
            lastPalm = currentPalm;
            switch (handType)
            {
                case Handedness.Right:
                    //Debug.Log("PA PoseDetector RightPalm : Current Palm = " + currentPalm + " PalmOpened event = " + PalmOpenedRight);
                    if (currentPalm && PalmOpenedRight != null)
                    {
                        //Debug.Log("PA PoseDetector Script : Palm Event Invoked");
                        //invoke the event
                        PalmOpenedRight.Invoke();
                    }
                    else if (!currentPalm && PalmNotOpenedRight != null)
                    {
                        //Debug.Log("PA PoseDetector Script : Palm Event contrary Invoked");
                        //invoke the event
                        PalmNotOpenedRight.Invoke();
                    }
                break;
                case Handedness.Left:
                    //Debug.Log("PA PoseDetector LeftPalm : Current Palm = " + currentPalm + " PalmOpened event = " + PalmOpenedLeft);
                    if (PalmOpenedLeft == null) Debug.Log("PA PoseDetector Bou : ciao");
                    if (currentPalm && PalmOpenedLeft != null)
                    {
                        //Debug.Log("PA PoseDetector PalmLeft : Palm Event Invoked");
                        //invoke the event
                        PalmOpenedLeft.Invoke();
                    }
                    else if (!currentPalm && PalmNotOpenedLeft != null)
                    {
                        //Debug.Log("PA PoseDetector PalmLeft : Palm Event Contrary Invoked");
                        //invoke the event
                        PalmNotOpenedLeft.Invoke();
                    }
                break;
                default:
                break;
            }
        }
    
    }
}
