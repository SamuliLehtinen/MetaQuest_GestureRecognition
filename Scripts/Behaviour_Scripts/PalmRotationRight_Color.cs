using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to change the color of a game object when the user has the right hand in a straight palm state
/// and the palm is facing up or down, but when nothing is detected, the color is set to normal
/// </summary>
public class PalmRotationRight_Color : MonoBehaviour
{
    [SerializeField]
    public Material colorNormal;
    [SerializeField]
    public Material colorPalmUp;
    [SerializeField]
    public Material colorPalmDown;

    RotationDetectorSingle rotationDetector;

    GameObject rightHand;

    private Renderer renderer;

    private void Awake()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        rotationDetector = rightHand.GetComponent<RotationDetectorSingle>();
        if (rotationDetector != null)
        {
            // Subscribe to the FistClosed event
            rotationDetector.palmUp_Palm += OnPalmUp_Palm;
            rotationDetector.palmDown_Palm += OnPalmDown_Palm;
            rotationDetector.palmNormal_Palm += OnPalmNormal_Palm;
            //Debug.Log("PA FistColorChange : subscription done.");
        }
    }

    private void OnPalmUp_Palm()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorPalmUp;
        }
    }

    private void OnPalmDown_Palm()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorPalmDown;
        }
    }

    private void OnPalmNormal_Palm()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorNormal;
        }
    }
}
