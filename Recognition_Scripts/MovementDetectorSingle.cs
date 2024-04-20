using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 


public class MovementDetectorSingle : MonoBehaviour
{

    PoseDetectorSingle poseDetector;


    // Define an enum for the direction
    public enum Handedness { Right, Left }

    public Handedness handType;

    [SerializeField]
    private OVRHand hand;

    [SerializeField]
    private OVRSkeleton handSkeleton;


    private bool isFistClosed;

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
        poseDetector = GetComponent<PoseDetectorSingle>();
        poseDetector.FistClosedRight += onFistClosedRight;

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

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isFistClosed)
        {
            Debug.Log("Fist is closed");
            calculateMovement();
        }
        
    }


    void calculateMovement()
    {
        poseDetector.FistClosedRight -= onFistClosedRight;
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
        }

        // TODO : calculate if there has been a movement of handBase.Transform.position on the z axis in less than 2 seconds
        Vector3 initialHandBasePos = handBase.Transform.position;

        DetectMovement(initialHandBasePos);

        
        Debug.Log("PA movement hand base: " + handBase.Transform.position);


    }

    // can not get into infinite looooops bcse it breaks the game
    void DetectMovement(Vector3 initialHandBasePos)
    {
        bool movementDetected = false;
        float movementThreshold = 0.3f;
        
        bool movementFinished = false;
        float initChronometer = 0.15f;
        // Check for movement until it's detected or 2 seconds have passed
        //while (!movementDetected)
        //{
            // Calculate current position of hand base
            Vector3 currentHandBasePos = handBase.Transform.position;

            float zInitDifference = Mathf.Abs(currentHandBasePos.z - initialHandBasePos.z);

            if(zInitDifference > initChronometer)
            {
                float startTime = Time.time;
                //while (!movementFinished)
                //{
                    // Calculate difference along z-axis
                    float zDifference = Mathf.Abs(currentHandBasePos.z - initialHandBasePos.z);

                    // Check if movement threshold is exceeded
                    if (zDifference > movementThreshold && Time.time - startTime < 2.0f)
                    {
                        Debug.Log("PA moveDetect Single : Hand movement detected!");
                        movementFinished = true;
                        movementDetected = true;
                    }
                    else if (Time.time - startTime > 2.0f)
                    {
                        Debug.Log("No movement detected");
                        movementFinished = true;
                    }
                //}
                
            }


        //}
    }
}