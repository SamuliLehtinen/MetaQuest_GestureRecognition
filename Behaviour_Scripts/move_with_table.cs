using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_with_table : MonoBehaviour
{
    // This script is used to move a game object around if it is touching a bounding box of a table
    PoseDetectorSingle poseDetector;

    GameObject leftHand;
    GameObject theTable;

    private bool movableCube = false;

    private bool startMovement = false;

    private bool touchesTable = false;

    private Vector3 handStartPosition;

    private Vector3 cubeStartPosition;   

    private float xdif, ydif, zdif;


    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");
        // the table not necessary to check
        theTable = GameObject.FindGameObjectWithTag("InitTable");
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
        if (touchesTable)
        {
            startMovement = true;
            cubeStartPosition = transform.position;
        }
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

    // This method is called when the collider of this object collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("InitTable"))
        {
            touchesTable = true;
        }
    }


    // This method is called when the collider of this object collides with another collider
    void OnCollisionExit(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("InitTable"))
        {
            touchesTable = false;
        }
       
    }
}
