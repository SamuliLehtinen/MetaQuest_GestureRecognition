using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to detect when user mimic the reading of a book with both hands having the palm opened
/// </summary>
public class PalmJoin : MonoBehaviour
{
    private bool arePalmsJoined = false;
    Vector3 vecDifference = new Vector3(0.0f, 0.0f, 0.0f);

    float fDifference = 0.0f;
    float zDifference = 0.0f;

    
    public bool processPalmJoin(OVRBone thumbTip_Right, OVRBone thumbTip_Left)
    {
        bool areThumbsTouching = false;
        
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
}
