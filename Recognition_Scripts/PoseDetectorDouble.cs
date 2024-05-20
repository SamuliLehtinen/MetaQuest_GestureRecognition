using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 


public class PoseDetectorDouble : MonoBehaviour
{
    // This script is used to raise events when the user performs a specific gesture with both hands
    // This works by subscribing to the events of the PoseDetectorSingle script
    private GameObject rightHand;
    private GameObject leftHand;

    [SerializeField]
    private OVRHand handRight;
    [SerializeField]
    private OVRHand handLeft;

    [SerializeField]
    private OVRSkeleton hand_Right_Skeleton;
    [SerializeField]
    private OVRSkeleton hand_Left_Skeleton;


    private FistClosed fistDetector;
    private PalmStraight palmDetector;
    private FistBump fistBumpDetector;
    private PalmJoin palmJoinDetector;

    private bool currentFistBump, lastFistBump;
    private bool currentPalmJoin, lastPalmJoin;

    public event Action FistBump;
    public event Action FistNoBump;

    public event Action PalmJoin;
    public event Action PalmNoJoin;

    OVRBone thumbTip_Right, thumbTip_Left;
    OVRBone indexTip_Right, indexTip_Left;
    OVRBone middleTip_Right, middleTip_Left;
    OVRBone ringTip_Right, ringTip_Left;
    OVRBone pinkyTip_Right, pinkyTip_Left;

    OVRBone handBase_Right, handBase_Left;
    OVRBone middleKnuckle_Right, middleKnuckle_Left;
    OVRBone middleMiddleKnuckle_Right, middleMiddleKnuckle_Left;
    OVRBone indexMiddleKnuckle_Right, indexMiddleKnuckle_Left;


    private bool rightFistClosed, leftFistClosed, bothFistClosed;
    private bool rightPalmOpened, leftPalmOpened, bothPalmOpened;

    private PoseDetectorSingle poseDetectorR, poseDetectorL;


    void Awake()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand_Prefab");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");

        fistBumpDetector = GetComponent<FistBump>();
        palmJoinDetector = GetComponent<PalmJoin>();


        if(!handRight) handRight = rightHand.GetComponent<OVRHand>();
        if(!handLeft) handLeft = leftHand.GetComponent<OVRHand>();


        if(!hand_Right_Skeleton) hand_Right_Skeleton = rightHand.GetComponent<OVRSkeleton>();
        if(!hand_Left_Skeleton) hand_Left_Skeleton = leftHand.GetComponent<OVRSkeleton>();


        if(handRight!=null) poseDetectorR = rightHand.GetComponent<PoseDetectorSingle>();
        if(handLeft!=null) poseDetectorL = leftHand.GetComponent<PoseDetectorSingle>();


        poseDetectorL.FistClosedLeft += OnFistClosedLeft;
        poseDetectorL.FistNotClosedLeft += OnFistNotClosedLeft;

        poseDetectorR.FistClosedRight += OnFistClosedRight;
        poseDetectorR.FistNotClosedRight += OnFistNotClosedRight;


        poseDetectorL.PalmOpenedLeft += OnPalmOpenedLeft;
        poseDetectorL.PalmNotOpenedLeft += OnPalmNotOpenLeft;

        poseDetectorR.PalmOpenedRight += OnPalmOpenedRight;
        poseDetectorR.PalmNotOpenedRight += OnPalmNotOpenRight;
    }


    // Start is called before the first frame update
    void Start()
    {
        rightFistClosed = false;
        leftFistClosed = false;
        bothFistClosed = false;

        rightPalmOpened = false;
        leftPalmOpened = false;
        bothPalmOpened = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (handRight.IsTracked)
        {

                thumbTip_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MaxSkinnable);
                indexTip_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbMetacarpal);
                middleTip_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip);
                ringTip_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_RingTip);
                pinkyTip_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbTip);

                handBase_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Start);
                middleMiddleKnuckle_Right = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_LeftArmUpper);
                middleKnuckle_Right = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_Middle1);

                indexMiddleKnuckle_Right = hand_Right_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Head);
        }
        if (handLeft.IsTracked)
        {
                thumbTip_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MaxSkinnable);
                indexTip_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbMetacarpal);
                middleTip_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip);
                ringTip_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_RingTip);
                pinkyTip_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Body_LeftHandThumbTip);

                handBase_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Start);
                
                middleMiddleKnuckle_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_LeftArmUpper);
                middleKnuckle_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.Hand_Middle1);

                indexMiddleKnuckle_Left = hand_Left_Skeleton.Bones.FirstOrDefault(bone => bone.Id == OVRSkeleton.BoneId.FullBody_Head);
        }


        if(rightFistClosed && leftFistClosed) bothFistClosed = true;
        else bothFistClosed = false;

        if(rightPalmOpened && leftPalmOpened) bothPalmOpened = true;
        else bothPalmOpened = false;    

        if(bothFistClosed)
        {
            // Polling of known hand poses 
            currentFistBump = fistBumpDetector.processFistBump(indexTip_Right, middleMiddleKnuckle_Right, ringTip_Left, middleMiddleKnuckle_Left);
            
            if (currentFistBump && FistBump != null)
            {
                //invoke the event
                FistBump.Invoke();
            }
            else
            {
                //invoke the event
                FistNoBump.Invoke();
            }
        }
        else
        {
            FistNoBump.Invoke();
        }

        if(bothPalmOpened)
        {
            currentPalmJoin = palmJoinDetector.processPalmJoin(pinkyTip_Right,pinkyTip_Left);
            if(currentPalmJoin && PalmJoin != null)
            {
                PalmJoin.Invoke();
            }
            else
            {
                PalmNoJoin.Invoke();
            }
        }
        else
        {
            PalmNoJoin.Invoke();
        }   
            
    }

    public void OnFistClosedRight()
    {
        rightFistClosed = true;
    }

    public void OnFistNotClosedRight()
    {
        rightFistClosed = false;
    }

    public void OnFistClosedLeft()
    {
        leftFistClosed = true;
    }

    public void OnFistNotClosedLeft()
    {
        leftFistClosed = false;
    }


    public void OnPalmOpenedRight()
    {
        rightPalmOpened = true;
    }

    public void OnPalmNotOpenRight()
    {
        rightPalmOpened = false;
    }

    public void OnPalmOpenedLeft()
    {
        leftPalmOpened = true;
    }
    public void OnPalmNotOpenLeft()
    {
        leftPalmOpened = false;
    }
}
