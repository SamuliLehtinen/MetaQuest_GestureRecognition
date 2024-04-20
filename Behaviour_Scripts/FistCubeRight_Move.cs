using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCubeRight_Move : MonoBehaviour
{

    PoseDetectorSingle poseDetector;

    GameObject rightHand;

    public float movementFactor = 2f;

    private bool movableCube = false;

    private bool startMovement = false;

    private Vector3 handStartPosition;

    private Vector3 cubeStartPosition;   

    private float xdif, ydif, zdif;


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

        cubeStartPosition = transform.position;
        
    }

    private void OnFistClosedRight()
    {
        
        startMovement = true;
    }

    private void OnFistNotClosedRight()
    {
        movableCube = false;
        transform.position = cubeStartPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (startMovement)
        {
            handStartPosition = rightHand.transform.position;
            startMovement = false;
            movableCube = true;
        }


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
}
