using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 

/// <summary>
/// This script is used to detect when the user performs a specific gesture with one hand only
/// It needs to be attached to an OVRHand object and OVRSkeleton component
/// </summary>

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
            //bones ID are the same for both hands
            // parameters for Right hand and for Left hand
            thumbTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MaxSkinnable);
            indexTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbMetacarpal);
            middleTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip);
            ringTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_RingTip);
            pinkyTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbTip);

            handBase = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Start);
            middleMiddleKnuckle = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_LeftArmUpper);

            indexMiddleKnuckle = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Head);
        }

        // Polling of known hand poses 
        currentFist = fistDetector.processFist(indexTip, middleTip, ringTip, pinkyTip, handBase, thumbTip, middleMiddleKnuckle);
        currentPalm = palmDetector.processPalm(indexTip, middleTip, ringTip, pinkyTip, handBase, thumbTip, indexMiddleKnuckle);
        
        // Detection of a change in the fist gesture
        if (currentFist != lastFist)
        {
            lastFist = currentFist;
            switch (handType)
            {
                case Handedness.Right:
                    if (currentFist && FistClosedRight != null)
                    {
                        //invoke the event
                        FistClosedRight.Invoke();
                    }
                    else if (!currentFist && FistNotClosedRight != null)
                    {
                        //invoke the event
                        FistNotClosedRight.Invoke();
                    }
                break;
                case Handedness.Left:
                    if (currentFist && FistClosedLeft != null)
                    {
                        //invoke the event
                        FistClosedLeft.Invoke();
                    }
                    else if (!currentFist && FistNotClosedLeft != null)
                    {
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
                    if (currentPalm && PalmOpenedRight != null)
                    {
                        //invoke the event
                        PalmOpenedRight.Invoke();
                    }
                    else if (!currentPalm && PalmNotOpenedRight != null)
                    {
                        //invoke the event
                        PalmNotOpenedRight.Invoke();
                    }
                break;
                case Handedness.Left:
                    if (currentPalm && PalmOpenedLeft != null)
                    {
                        //invoke the event
                        PalmOpenedLeft.Invoke();
                    }
                    else if (!currentPalm && PalmNotOpenedLeft != null)
                    {
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
