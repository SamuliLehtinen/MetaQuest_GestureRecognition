using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palmColorChangeLeft : MonoBehaviour
{
    [SerializeField]
    public Material colorPalmOpened;
    [SerializeField]
    public Material colorPalmClosed;

    PoseDetectorSingle poseDetector;

    GameObject leftHand;

    private Renderer colorChanger;

    private void Awake()
    {
        // Get the renderer component
        colorChanger = GetComponent<Renderer>();
    }

    void Start()
    {
        // Get the renderer component
        colorChanger.material = colorPalmClosed;
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        poseDetector = leftHand.GetComponent<PoseDetectorSingle>();
        //Debug.Log("PA palmColorLeft start : poseDetector found " + poseDetector);

        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.PalmOpenedLeft += OnPalmOpenedLeft;
            poseDetector.PalmNotOpenedLeft += OnPalmNotOpenedLeft;
            //Debug.Log("PA palmColorLeft : subscription to left Palm event done.");
        }
    }

    private void OnPalmOpenedLeft()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
            colorChanger.material = colorPalmOpened;
        }
    }

    private void OnPalmNotOpenedLeft()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
            colorChanger.material = colorPalmClosed;
            //Debug.Log("PA palmColorChangeLeft : Palm Closed");
        }
    }

    void Update()
    {

        
    }
    
}
