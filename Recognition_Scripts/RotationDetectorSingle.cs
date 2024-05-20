using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 

public class RotationDetectorSingle : MonoBehaviour
{
    // This script is used to raise a rotation event when the user performs a specific gesture with a rotation with one hand only
    // It works by subscribing to the events of the PoseDetectorSingle script
    // It needs to be attached to an OVRHand object and OVRSkeleton component

    PoseDetectorSingle poseDetector;
    // Define an enum for the direction
    public enum Handedness { Right, Left }

    public Handedness handType;

    [SerializeField]
    private OVRHand hand;

    [SerializeField]
    private OVRSkeleton handSkeleton;

    private bool hasBeenInvoked;

    private bool isFistClosed;
    private bool isFistNotClosed;

    private bool isPalmStraight;
    private bool isPalmNotStraight;

    private bool isPalmUp;
    private bool isPalmDown;

    OVRBone thumbTip;
    OVRBone indexTip;
    OVRBone middleTip;
    OVRBone ringTip;
    OVRBone pinkyTip;

    OVRBone handBase;
    OVRBone middleKnuckle;
    OVRBone middleMiddleKnuckle;
    OVRBone indexMiddleKnuckle;

    public event Action palmUp_Fist;
    public event Action palmDown_Fist;
    public event Action palmNormal_Fist;

    public event Action palmUp_Palm;
    public event Action palmDown_Palm;
    public event Action palmVertical_Palm;
    public event Action palmNormal_Palm;


    void Awake()
    {
        hasBeenInvoked = false;
        isFistClosed = false;
        isFistNotClosed = false;
        isPalmStraight = false;
        isPalmNotStraight = false;
        poseDetector = GetComponent<PoseDetectorSingle>();
        switch(handType)
        {
            case Handedness.Right:
                poseDetector.FistClosedRight += onFistClosedRight;
                poseDetector.FistNotClosedRight += onFistNotClosedRight;
                poseDetector.PalmOpenedRight += onPalmStraightRight;
                poseDetector.PalmNotOpenedRight += onPalmNotStraightRight;
                break;
            case Handedness.Left:
                poseDetector.FistClosedLeft += onFistClosedLeft;
                poseDetector.FistNotClosedLeft += onFistNotClosedLeft;
                poseDetector.PalmOpenedLeft += onPalmStraightLeft;
                poseDetector.PalmNotOpenedLeft += onPalmNotStraightLeft;
                break;
        }
        
        

        if(!hand) hand = GetComponent<OVRHand>();
        if(!handSkeleton) 
        {
            handSkeleton = GetComponent<OVRSkeleton>();
        }
    }

    void onFistClosedRight()
    {
        isFistClosed = true;
    }

    void onFistNotClosedRight()
    {
        isFistClosed = false;
    }
    
    void onFistClosedLeft()
    {
        isFistClosed = true;
    }

    void onFistNotClosedLeft()
    {
        isFistClosed = false;
    }

    void onPalmStraightRight()
    {
        isPalmStraight = true;
    }
    void onPalmNotStraightRight()
    {
        isPalmStraight = false;
    }
    void onPalmStraightLeft()
    {
        isPalmStraight = true;
    }
    void onPalmNotStraightLeft()
    {
        isPalmStraight = false;
    }


    void calculateRotation(int which_event)
    {
       
        if(hand.IsTracked)
        {
            handBase = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Start);
            middleTip = handSkeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip);

            Debug.Log("PA rotation base euler :" + handBase.Transform.rotation.eulerAngles);
            Debug.Log("PA rotation middle euler :" + middleTip.Transform.rotation.eulerAngles);
            //palm down 0 < z coordinate < 20 - palm up 150 < z coordinate < 180
            //need a switch because the rotation coordinates is mirrored for left and right hands
            switch(handType)
            {
                case Handedness.Right:
                        if(handBase.Transform.rotation.eulerAngles.z > 0 && handBase.Transform.rotation.eulerAngles.z < 20)
                        {
                            hasBeenInvoked = false;
                            switch(which_event)
                            {
                                case 1:
                                    if(palmDown_Fist != null)
                                    {
                                        palmDown_Fist.Invoke();
                                    }
                                    break;
                                case 2:
                                    if(palmDown_Palm != null)
                                    {
                                        palmDown_Palm.Invoke();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        // coordinates to detect palm pointing up to the sky
                        else if(handBase.Transform.rotation.eulerAngles.z > 150 && handBase.Transform.rotation.eulerAngles.z < 180)
                        {
                            hasBeenInvoked = false;
                            switch(which_event)
                            {
                                case 1:
                                    if(palmUp_Fist != null)
                                    {
                                        palmUp_Fist.Invoke();
                                    }
                                    break;
                                case 2:
                                    if(palmUp_Palm != null)
                                    {
                                        palmUp_Palm.Invoke();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            if(hasBeenInvoked == false)
                            {
                                hasBeenInvoked = true;
                                switch(which_event)
                                {
                                    case 1:
                                        if(palmNormal_Fist != null)
                                        {
                                            palmNormal_Fist.Invoke();
                                        }
                                        break;
                                    case 2:
                                        if(palmNormal_Palm != null)
                                        {
                                            palmNormal_Palm.Invoke();
                                        }
                                        break;
                                    default:    
                                        break;
                                }
                            }
                        }
                break;
                case Handedness.Left:
                    if(handBase.Transform.rotation.eulerAngles.z > 340 || (handBase.Transform.rotation.eulerAngles.z > 0 && handBase.Transform.rotation.eulerAngles.z < 10))
                    {
                        hasBeenInvoked = false;
                        switch(which_event)
                        {
                            case 1:
                                if (palmUp_Fist != null)
                                {
                                    palmUp_Fist.Invoke();
                                }
                                break;
                            case 2:
                                if (palmUp_Palm != null)
                                {
                                    palmUp_Palm.Invoke();
                                }
                                break;
                        }
                    }
                    else if(handBase.Transform.rotation.eulerAngles.z > 180 && handBase.Transform.rotation.eulerAngles.z < 200)
                    {
                        hasBeenInvoked = false;
                        switch(which_event)
                        {
                            case 1:
                                if(palmDown_Fist != null)
                                {
                                    palmDown_Fist.Invoke();
                                }
                                break;
                            case 2:
                                if(palmDown_Palm != null)
                                {
                                    palmDown_Palm.Invoke();
                                }
                                break;
                        }
                    }
                    else
                    {
                    if(hasBeenInvoked == false)
                        {
                            hasBeenInvoked = true;
                            switch(which_event)
                            {
                                case 1:
                                    if(palmNormal_Fist != null)
                                    {
                                        palmNormal_Fist.Invoke();
                                    }
                                    break;
                                case 2:
                                    if(palmNormal_Palm != null)
                                    {
                                        palmNormal_Palm.Invoke();
                                    }
                                    break;
                                default:    
                                    break;
                            }
                        }
                    }
                    
                break;
                default:
                    break;
            }
            

        }
       
    }

    // Update is called once per frame
    void Update()
    {
        // after duplicating for palm opened find a way to not copy paste all code 
        //so changed "calculateRotation()" to "calculateRotation(int which_event)"
        if(isFistClosed)
        {
            calculateRotation(1);
        }
        if(isPalmStraight)
        {
            calculateRotation(2);
        }
    }
}
