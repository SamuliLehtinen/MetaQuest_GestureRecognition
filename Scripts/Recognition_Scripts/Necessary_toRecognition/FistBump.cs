using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to detect if the user is performing a fist bump gesture
/// </summary>
public class FistBump : MonoBehaviour
{
    private bool areFistsBumped = false;
    Vector3 vecDifference = new Vector3(0.0f, 0.0f, 0.0f);
    float fDifference = 0.0f;
    float zDifference = 0.0f;
    
    // This function is used to calculate distance between the knuckles of right and left hand when the hands are in a fist closed state
    // Distance of base of the middle finger and the middle of the middle finger
    public bool processFistBump(OVRBone middleKnuckle_Right, OVRBone middleMiddleKnuckle_Right, OVRBone middleKnuckle_Left, OVRBone middleMiddleKnuckle_Left)
    {
        
        bool isMiddleKnuckleTouching = false;
        bool isMiddleMiddleKnuckleTouching = false;

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
            isMiddleMiddleKnuckleTouching = true;
        }
        else
        {
            isMiddleMiddleKnuckleTouching = false;
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
}
