using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust_object : MonoBehaviour
{
    // This script is used to move around a game object when the user has a straight palm
    PoseDetectorSingle poseDetector;
    RotationDetectorSingle rotationDetector;
    GameObject rightHand;
    private bool movableCube = false;
    private bool startMovement = false;
    private Vector3 handStartPosition;
    private Vector3 cubeStartPosition;   
    private float xdif, ydif, zdif;
    private bool touchesTable = false;


    // Start is called before the first frame update
    void Start()
    {
        touchesTable = false;
        rightHand = GameObject.FindGameObjectWithTag("RightHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        poseDetector = rightHand.GetComponent<PoseDetectorSingle>();
        rotationDetector = rightHand.GetComponent<RotationDetectorSingle>();

        if (poseDetector != null)
        {
            poseDetector.PalmOpenedRight += OnPalmOpenedRight;
            poseDetector.PalmNotOpenedRight += OnPalmNotOpenedRight;
            //poseDetector.FistClosedRight += OnFistClosedRight;
            //poseDetector.FistNotClosedRight += OnFistNotClosedRight;
        }

        
    }

    private void OnPalmOpenedRight()
    {
        Debug.Log("PA adjust : Fist closed, touches table = " + touchesTable);
        if (touchesTable)
        {
            startMovement = true;
            cubeStartPosition = transform.position;
        }
    }

    private void OnPalmNotOpenedRight()
    {
        if (touchesTable)
        {
            movableCube = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // store the initial position of the hand at beginning of the movement
        if (startMovement)
        {
            handStartPosition = rightHand.transform.position;
            startMovement = false;
            movableCube = true;
        }

        // Once the position is stored, the object can be moved
        if (movableCube)
        {
            //retrieve the change of the hand position 
            Vector3 handPosition = rightHand.transform.position;
            //translate the change of the position of the hand to the position of the cube
            xdif = handPosition.x - handStartPosition.x;
            ydif = handPosition.y - handStartPosition.y;
            zdif = handPosition.z - handStartPosition.z;
            //update the position of the cube only on the x and z axis
            transform.position = new Vector3(cubeStartPosition.x + xdif, cubeStartPosition.y, cubeStartPosition.z + zdif);
        }
    }

    // This method is used to detect if the object is on the final table or not
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PA collision : Collided with object with tag: " + collision.collider.tag);
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("finalTable"))
        {
            touchesTable = true;
        }
    }


    // This method is used to detect if the object is on the final table or not
    void OnCollisionExit(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("finalTable"))
        {
            touchesTable = false;
        }
       
    }
    
}
