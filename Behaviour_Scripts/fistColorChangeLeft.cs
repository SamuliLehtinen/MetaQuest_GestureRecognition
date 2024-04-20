using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fistColorChangeLeft : MonoBehaviour
{
    [SerializeField]
    public Material colorFistClosed;
    [SerializeField]
    public Material colorFistOpened;

    PoseDetectorSingle poseDetector;

    GameObject leftHand;

    private Renderer renderer;

    private void Awake()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        // Get the renderer component
        renderer.material = colorFistOpened;
        Debug.Log("PA FistColorChange init : color2 set.");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");
        // Find the PoseDetector script and subscribe to its events
        poseDetector = leftHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.FistClosedLeft += OnFistClosedLeft;
            poseDetector.FistNotClosedLeft += OnFistNotClosedLeft;
            Debug.Log("PA FistColorChange 123: subscription done.");
        }
    }

    private void OnFistClosedLeft()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorFistClosed;
        }
    }

    private void OnFistNotClosedLeft()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorFistOpened;
        }
    }

    void Update()
    {

        
    }
}
