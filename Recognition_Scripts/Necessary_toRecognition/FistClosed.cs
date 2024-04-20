using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems.OVRInputModule;


public class FistClosed : MonoBehaviour
{
    private OVRHand rightHand;
    //private HandFinger rightHandFinger;
    private bool isFistClosed = false;

    [SerializeField]
    public GameObject fistCube;
    [SerializeField]
    public Material Material1;
    [SerializeField]
    public Material Material2;


    void Awake()
    {
        //rightHand = GetComponent<OVRHand>();
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

    
    public bool processFist(OVRBone indexTip, OVRBone middleTip, OVRBone ringTip, OVRBone pinkyTip, OVRBone handBase, OVRBone thumbTip, OVRBone middleMiddleKnuckle)
    {

        //Debug.Log("PA FC Index knucle, Z : " + indexTip.position + " " + indexKnuckle.position);
        //LOOP To check finger tip position can not work because if only ring finger open then it will be considered as closed
        //because index puts one bool variable to true ... ring puts one variable to false, but pinky reset to true. 
        //So must check 5 finger of the hand individually 
        
        bool isIndexTipClosed = false;
        bool isMiddleTipClosed = false;
        bool isRingTipClosed = false;
        bool isPinkyTipClosed = false;


        //Check if the index tip is closed
        if (Vector3.Distance(indexTip.Transform.position, middleTip.Transform.position) < 0.1f)
        {
            //Debug.Log("PA FC Index : Index is closed");
            isIndexTipClosed = true;
        }
        else
        {
            isIndexTipClosed = false;
            //Debug.Log("PA FC Index : Index is opened");
        }
        //Check if the middle tip is closed
        if (Vector3.Distance(middleTip.Transform.position, ringTip.Transform.position) < 0.1f)
        {
            //Debug.Log("PA FC Index : Middle is closed");
            isMiddleTipClosed = true;
        }
        else
        {
            isMiddleTipClosed = false;
            //Debug.Log("PA FC Index : Middle is opened");
        }
        //Check if the ring tip is closed
        if (Vector3.Distance(ringTip.Transform.position, pinkyTip.Transform.position) < 0.1f)
        {
            //Debug.Log("PA FC Index : Ring is closed");
            isRingTipClosed = true;
        }
        else
        {
            //Debug.Log("PA FC Index : Ring is opened");
            isRingTipClosed = false;    
        }
        //Check if the pinky tip is closed
        if (Vector3.Distance(pinkyTip.Transform.position, handBase.Transform.position) < 0.1f)
        {
            //Debug.Log("PA FC Index : Pinky is closed");
            isPinkyTipClosed = true;
        }
        else
        {
            
            //Debug.Log("PA FC Index : Pinky is opened");
            isPinkyTipClosed = false;
        }

        //Check if the thumb tip is closed
        bool isThumbTipClosed = false;

        fDifference = Vector3.Distance(thumbTip.Transform.position, middleMiddleKnuckle.Transform.position);
        //Debug.Log("PA FC thumb floatDifference (thumbTip - middlemiddle) : " + fDifference);

        if(fDifference < 0.03f)
        {
            //Debug.Log("PA FC Thumb : Thumb is closed");
            isThumbTipClosed = true; 
        }
        else
        {
            //Debug.Log("PA FC Thumb : Thumb is opened");
            isThumbTipClosed = false;
        }



        if(isIndexTipClosed && isMiddleTipClosed && isRingTipClosed && isPinkyTipClosed && isThumbTipClosed)
        {
            //Debug.Log("PA FC Index : Fist is closed");
            //fistCube.GetComponent<Renderer>().material = Material1;
            return true;
        }
        else
        {
            //fistCube.GetComponent<Renderer>().material = Material2;
            //Debug.Log("PA FC Index : Fist is opened");
            return false;
        }
    }


    

    // Update is called once per frame
    void Update()
    {
        // Check if the hand is in the fist closed state
        // isFistClosed = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }
}
