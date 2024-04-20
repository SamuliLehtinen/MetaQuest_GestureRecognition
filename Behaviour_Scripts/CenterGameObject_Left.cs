using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterGameObject_Left : MonoBehaviour
{

    public string GameObjectName = "EmperorAngelfish";
    public float objectHeight;

    RotationDetectorSingle rotationDetector;
    PoseDetectorSingle positionDetector;

    GameObject leftHand;
    GameObject centeredObject;

    private bool centerObjectPosition = false;

    // Start is called before the first frame update
    void Start()
    {
        objectHeight = 0.3f;

        centeredObject = GameObject.Find(GameObjectName);
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");
        rotationDetector = leftHand.GetComponent<RotationDetectorSingle>();
        positionDetector = leftHand.GetComponent<PoseDetectorSingle>();
        if (rotationDetector != null && positionDetector != null)
        {
            // Subscribe to the FistClosed event
            rotationDetector.palmUp_Palm += OnPalmUp_Palm;
            rotationDetector.palmNormal_Palm += OnPalmNormal_Palm;
            positionDetector.PalmNotOpenedLeft += OnPalmNormal_Palm;
        }
        
    }

    private void OnPalmUp_Palm()
    {
        centerObjectPosition = true;
    }
    private void OnPalmNormal_Palm()
    {
        centerObjectPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (centerObjectPosition)
        {
            Vector3 handPosition = leftHand.transform.position; 
            centeredObject.transform.position = new Vector3(handPosition.x, handPosition.y + objectHeight, handPosition.z);
        }   
    }
}
