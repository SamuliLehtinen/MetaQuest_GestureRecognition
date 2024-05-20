using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to move a game object with the left hand in a fist state
/// </summary>
public class L_Fist_Movement : MonoBehaviour
{
    PoseDetectorSingle poseDetector;

    GameObject leftHand;

    private bool movableCube = false;

    private bool startMovement = false;

    private Vector3 handStartPosition;

    private Vector3 cubeStartPosition;   

    private float xdif, ydif, zdif;


    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");
        // Find the PoseDetector script and subscribe to its events
        poseDetector = leftHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            poseDetector.FistClosedLeft += FistClosedLeft;
            poseDetector.FistNotClosedLeft += FistNotClosedLeft;
        }
    }

    private void FistClosedLeft()
    {
        startMovement = true;
        cubeStartPosition = transform.position;
    }

    private void FistNotClosedLeft()
    {
        movableCube = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMovement)
        {
            handStartPosition = leftHand.transform.position;
            startMovement = false;
            movableCube = true;
        }


        if (movableCube)
        {
            //retrieve the change of the hand position 
            Vector3 handPosition = leftHand.transform.position;
            //translate the change of the position of the hand to the position of the cube
            xdif = handPosition.x - handStartPosition.x;
            ydif = handPosition.y - handStartPosition.y;
            zdif = handPosition.z - handStartPosition.z;
            //update the position of the cube only on the x and z axis
            transform.position = new Vector3(cubeStartPosition.x + xdif, cubeStartPosition.y, cubeStartPosition.z + zdif);
        }
    }
}
