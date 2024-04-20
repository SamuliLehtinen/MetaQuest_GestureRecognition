using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandDebugBoneInfo : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI boneText;

    private OVRBone bone;

    private OVRSkeleton skeleton;
    
    Vector3 handBase = new Vector3(0.0f, 0.0f, 0.0f);
    
    Vector3 vecDifference = new Vector3(0.0f, 0.0f, 0.0f);
    float fDifference = 0.0f;
    Vector3 thumbTip = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 indexTip;
    Vector3 middleTip = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 ringTip = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 pinkyTip = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 middleOfMiddleFinger = new Vector3(0.0f, 0.0f, 0.0f);

    Vector3 indexKnuckle;
    Vector3 middleKnuckle = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 ringKnuckle = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 pinkyKnuckle = new Vector3(0.0f, 0.0f, 0.0f);


    float indexTipZ;
    float indexKnuckleZ;
    float zDifference = 0.0f;
    
    
    void Start()
    {
        indexKnuckle = new Vector3(0.0f, 0.0f, 0.0f);
        indexKnuckleZ = 0.0f;
        indexTip = new Vector3(0.0f, 0.0f, 0.0f);
        indexTipZ = 0.0f;
    }

    public void AddBone(OVRBone bone) => this.bone = bone;

    // Update is called once per frame
    void Update()
    {
        if (bone==null) return;
        
        boneText.text = $"{bone.Id}";
        boneText.transform.rotation = Quaternion.LookRotation(boneText.transform.position - Camera.main.transform.position, Vector3.up);
        transform.position = bone.Transform.position;
        transform.rotation = bone.Transform.rotation;
                                                        //Thumb                 //Index                  //Middle                 //Ring                  //Pinky            //Middle of Middle Finger
        List<string> boneTips = new List<string> { "Hand_MaxSkinnable",  "Body_LeftHandThumbMetacarpal", "Hand_MiddleTip",  "Hand_RingTip", "Body_LeftHandThumbTip", "FullBody_LeftArmUpper"};
        
        string wristBase = "FullBody_Start";
                                                    //index         middle         ring                            Pinky   
        List<string> knuckles = new List<string> {"Hand_Index1", "Hand_Middle1", "Body_LeftHandWristTwist", "Body_RightArmLower"};
        string boneName = $"{bone.Id}";

        //first only update the index knuckle position 
        if(boneName ==  "Hand_Index1")
        {
            indexKnuckle = bone.Transform.position;
            indexKnuckleZ = bone.Transform.position.z;
            //Debug.Log("PA Index knucle, Z : " + indexKnuckle + " " + indexKnuckleZ);
        }
        //then update the index tip position + calculate the difference between tip and knuckle
        if(boneName ==  "Body_LeftHandThumbMetacarpal")
        {
            //Debug.Log("PA indexKnuckle, Z IC2 : " + indexKnuckle + " " + indexKnuckleZ);

            indexTip = bone.Transform.position;
            indexTipZ = bone.Transform.position.z;

            vecDifference = indexTip - indexKnuckle;
            fDifference = Vector3.Distance(indexTip, indexKnuckle);
            zDifference = indexTipZ - indexKnuckleZ;
            //Debug.Log("PA indexTip : " + indexTip);
            //Debug.Log("PA floatDifference (tip - knuckle) : " + fDifference);
            //Debug.Log("PA Vector3Difference (tip - knuckle) : " + vecDifference);
            //Debug.Log("PA Zdifference : " + zDifference);
        }
        

        //vecDifference = indexTip - indexKnuckle;
        //fDifference = Vector3.Distance(indexTip, indexKnuckle);
        //zDifference = indexTipZ - indexKnuckleZ;
        //outside of if just gives 0 
        //Debug.Log("PA indexTip OC: " + indexTip);
        //Debug.Log("PA indexKnuckle OC: " + indexKnuckle);
        //Debug.Log("PA floatDifference (tip - knuckle) OC : " + fDifference);
        //Debug.Log("PA Vector3Difference (tip - knuckle) OC : " + vecDifference);
        //Debug.Log("PA Zdifference OC: " + zDifference);
        
        
    }
}
