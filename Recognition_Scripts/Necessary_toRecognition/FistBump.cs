using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems.OVRInputModule;


public class FistBump : MonoBehaviour
{
    
    //private HandFinger rightHandFinger;
    private bool areFistsBumped = false;


    void Awake()
    {
        
    }
    // Start is called before the first frame update
    //void Start()
    //{
        // Get the right hand controller
        //rightHand = GetComponent<OVRHand>();
    //}
    Vector3 vecDifference = new Vector3(0.0f, 0.0f, 0.0f);

    float fDifference = 0.0f;
    float zDifference = 0.0f;

    
    public bool processFistBump(OVRBone middleKnuckle_Right, OVRBone middleMiddleKnuckle_Right, OVRBone middleKnuckle_Left, OVRBone middleMiddleKnuckle_Left)
    {

        //Transform wristBase = handBase.Transform;
        
        //Debug.Log("PA FC Index knucle, Z : " + indexTip.position + " " + indexKnuckle.position);
        //LOOP To check finger tip position can not work because if only ring finger open then it will be considered as closed
        //because index puts one bool variable to true ... ring puts one variable to false, but pinky reset to true. 
        //So must check 5 finger of the hand individually 
        
        bool isMiddleKnuckleTouching = false;
        bool isMiddleMiddleKnuckleTouching = false;
        

        Debug.Log("PA FistBump Distance knuckle: " + Vector3.Distance(middleKnuckle_Right.Transform.position, middleKnuckle_Left.Transform.position));
        Debug.Log("PA FistBump Distance middle knuckle: " + Vector3.Distance(middleMiddleKnuckle_Right.Transform.position, middleMiddleKnuckle_Left.Transform.position));

        if (Vector3.Distance(middleKnuckle_Right.Transform.position, middleKnuckle_Left.Transform.position) < 0.11f)
        {
            isMiddleKnuckleTouching = true;
        }
        else
        {
            isMiddleKnuckleTouching = false;
        }
        //Check if the middle tip is closed
        if (Vector3.Distance(middleMiddleKnuckle_Right.Transform.position, middleMiddleKnuckle_Left.Transform.position) < 0.11f)
        {
            //Debug.Log("PA FC Index : Middle is closed");
            isMiddleMiddleKnuckleTouching = true;
        }
        else
        {
            isMiddleMiddleKnuckleTouching = false;
            //Debug.Log("PA FC Index : Middle is opened");
        }
        
        if(isMiddleKnuckleTouching && isMiddleMiddleKnuckleTouching)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    

    // Update is called once per frame
    void Update()
    {
        // Check if the hand is in the fist closed state
        
        //isFistClosed = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }
}
