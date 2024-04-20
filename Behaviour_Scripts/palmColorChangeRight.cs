using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palmColorChangeRight : MonoBehaviour
{
    [SerializeField]
    public Material PalmOpened;
    [SerializeField]
    public Material PalmClosed;

    PoseDetectorSingle poseDetector;

    GameObject rightHand;

    private Renderer renderer;

    private void Awake()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        // Get the renderer component
        renderer.material = PalmClosed;
        // Debug.Log("PA FistColorChange init : color2 set.");
        rightHand = GameObject.FindGameObjectWithTag("RightHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        poseDetector = rightHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.PalmOpenedRight += OnPalmOpenedRight;
            poseDetector.PalmNotOpenedRight += OnPalmNotOpenedRight;
            //Debug.Log("PA FistColorChange : subscription done.");
        }
    }

    private void OnPalmOpenedRight()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = PalmOpened;
        }
    }

    private void OnPalmNotOpenedRight()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = PalmClosed;
        }
    }

    void Update()
    {

        
    }
    
}
