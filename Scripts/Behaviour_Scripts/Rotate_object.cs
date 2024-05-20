using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to rotate a game object when the user has a right closed fist and the object touches the final table
/// </summary>
public class Rotate_object : MonoBehaviour
{
    PoseDetectorSingle poseDetector;

    GameObject rightHand;

    private bool movableCube = false;

    private bool startMovement = false;

    private Vector3 handStartRotation;

    private Vector3 cubeStartRotation;   

    private float rota_dif;

    private bool touchesTable = false;


    // Start is called before the first frame update
    void Start()
    {

        rightHand = GameObject.FindGameObjectWithTag("RightHand_Prefab");
        // Find the PoseDetector script and subscribe to its events
        poseDetector = rightHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            poseDetector.FistClosedRight += OnFistClosedRight;
            poseDetector.FistNotClosedRight += OnFistNotClosedRight;
        }
      
    }
    
    private void OnFistClosedRight()
    {
        if (touchesTable)
        {
            startMovement = true;
            cubeStartRotation = transform.eulerAngles;
        }
    }

    private void OnFistNotClosedRight()
    {
        if (touchesTable)
        {
            movableCube = false;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (startMovement)
        {
            handStartRotation = rightHand.transform.eulerAngles;
            startMovement = false;
            movableCube = true;
        }


        if (movableCube)
        {
            //retrieve the change of the hand position 
            Vector3 handrotation = rightHand.transform.eulerAngles;
            //translate the change of the position of the hand to the position of the cube
            rota_dif = handrotation.y - handStartRotation.y;
            //update the position of the cube only on the x and z axis
            transform.eulerAngles = new Vector3(cubeStartRotation.x, cubeStartRotation.y + rota_dif, cubeStartRotation.z);
        }
    }

    // This method is called when the collider of this object collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("finalTable"))
        {
            touchesTable = true;
        }
    }


    // This method is called when the collider of this object collides with another collider
    void OnCollisionExit(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("finalTable"))
        {
            touchesTable = false;
        }
       
    }
}
