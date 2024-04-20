using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems.OVRInputModule;


public class PalmJoin : MonoBehaviour
{
    
    //private HandFinger rightHandFinger;
    private bool arePalmsJoined = false;


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

    
    public bool processPalmJoin(OVRBone thumbTip_Right, OVRBone thumbTip_Left)
    {

        //Transform wristBase = handBase.Transform;
        
        //Debug.Log("PA FC Index knucle, Z : " + indexTip.position + " " + indexKnuckle.position);
        //LOOP To check finger tip position can not work because if only ring finger open then it will be considered as closed
        //because index puts one bool variable to true ... ring puts one variable to false, but pinky reset to true. 
        //So must check 5 finger of the hand individually 
        
        bool areThumbsTouching = false;
        

        //Debug.Log("PA PalmsJoined pinkyDistance : " + Vector3.Distance(thumbTip_Right.Transform.position, thumbTip_Left.Transform.position));
        //Check if the index tip is closed
        if (Vector3.Distance(thumbTip_Right.Transform.position, thumbTip_Left.Transform.position) < 0.02f)
        {
            areThumbsTouching = true;
        }
        else
        {
            areThumbsTouching = false;
        }
        
        
        if(areThumbsTouching)
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
