using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 


public class HandDebugBoneDisplay : MonoBehaviour
{

    [SerializeField]
    private OVRHand hand;

    [SerializeField]
    private OVRSkeleton handSkeleton;

    [SerializeField]
    private GameObject pointerPosePrefab;

    [SerializeField]
    private GameObject bonePrefab;

    private Transform pointerPose;
    private bool bonesAdded;

    private void Awake()
    {
        if(!hand) hand = GetComponent<OVRHand>();
        if(!handSkeleton) 
        {
            handSkeleton = GetComponent<OVRSkeleton>();
        }
    }

    private void CreateBones()
    {
        switch(handSkeleton.GetSkeletonType())
        {
            case OVRSkeleton.SkeletonType.HandRight:
                Debug.Log("PA HandDebugBoneDisplay : Right Hand");
                                                            //Thumb                 //Index                  //Middle                 //Ring                  //Pinky             //Hand base       //middle finger middle knuckle
                List<string> stringListRight = new List<string> { "Hand_MaxSkinnable",  "Body_LeftHandThumbMetacarpal", "Hand_MiddleTip",  "Hand_RingTip", "Body_LeftHandThumbTip", "FullBody_Start", "FullBody_LeftArmUpper"};
                List<string> knucklesRight = new List<string> {"Hand_Index1", "Hand_Middle1", "Body_LeftHandWristTwist", "Body_RightArmLower", "FullBody_Head"};
                //Transform[] transformArrayRight = new Transform[2];

                foreach(var bone in handSkeleton.Bones)
                {
                    string boneName = bone.Id.ToString();
                    if (stringListRight.Contains(boneName) || knucklesRight.Contains(boneName))
                    {
                    Instantiate(bonePrefab, bone.Transform).GetComponent<HandDebugBoneInfo>().AddBone(bone);
                    }

                }
            break;
            case OVRSkeleton.SkeletonType.HandLeft:
                Debug.Log("PA HandDebugBoneDisplay : Right Hand");
                                                            //Thumb                 //Index                  //Middle                 //Ring                  //Pinky             //Hand base       //middle finger middle knuckle
                List<string> stringListLeft = new List<string> { "Hand_MaxSkinnable",  "Body_LeftHandThumbMetacarpal", "Hand_MiddleTip",  "Hand_RingTip", "Body_LeftHandThumbTip", "FullBody_Start", "FullBody_LeftArmUpper"};
                List<string> knucklesLeft = new List<string> {"Hand_Index1", "Hand_Middle1", "Body_LeftHandWristTwist", "Body_RightArmLower", "FullBody_Head"};
                //Transform[] transformArrayLeft = new Transform[2];

                foreach(var bone in handSkeleton.Bones)
                {
                    string boneName = bone.Id.ToString();
                    if (stringListLeft.Contains(boneName) || knucklesLeft.Contains(boneName))
                    {
                    Instantiate(bonePrefab, bone.Transform).GetComponent<HandDebugBoneInfo>().AddBone(bone);
                    }

                }
            break;
            default:
            break;
            
        }
        
        bonesAdded = true;
    }

    private void Update()
    {
        if (hand.IsTracked)
        {
            if(!pointerPose)
            {
                //pointerPose = Instantiate(pointerPosePrefab).transform;
            }
            if(hand.IsPointerPoseValid)
            {
                pointerPose.position = hand.PointerPose.position;
                pointerPose.rotation = hand.PointerPose.rotation;
            }
                
            if(!bonesAdded)
            {
                CreateBones();
            }

        }
    }
    
}
