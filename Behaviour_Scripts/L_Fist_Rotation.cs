using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Fist_Rotation : MonoBehaviour
{
    // This script is used to rotate a game object on the z axis with the left hand in a fist state
    PoseDetectorSingle poseDetector;

    GameObject leftHand;

    private bool movableCube = false;

    private bool startMovement = false;

    private Vector3 handStartRotation;

    private Vector3 cubeStartRotation;   

    private float rota_dif;


    // Start is called before the first frame update
    void Start()
    {

        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");
        // Find the PoseDetector script and subscribe to its events
        poseDetector = leftHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            poseDetector.FistClosedLeft += OnFistClosedLeft;
            poseDetector.FistNotClosedLeft += OnFistNotClosedLeft;
        }
      
    }
    
    private void OnFistClosedLeft()
    {
        startMovement = true;
        cubeStartRotation = transform.eulerAngles;
    }

    private void OnFistNotClosedLeft()
    {
        movableCube = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (startMovement)
        {
            handStartRotation = leftHand.transform.eulerAngles;
            startMovement = false;
            movableCube = true;
        }


        if (movableCube)
        {
            //retrieve the change of the hand rotation 
            Vector3 handrotation = leftHand.transform.eulerAngles;
            //translate the change of the position of the hand to the position of the cube
            rota_dif = handrotation.y - handStartRotation.y;
            //update the position of the cube only on the x and z axis
            transform.eulerAngles = new Vector3(cubeStartRotation.x, cubeStartRotation.y + rota_dif, cubeStartRotation.z);
        }
    }
}
