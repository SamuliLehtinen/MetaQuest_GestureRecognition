using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistRotationLeft_Color : MonoBehaviour
{
    [SerializeField]
    public Material colorNormal;
    [SerializeField]
    public Material colorPalmUp;
    [SerializeField]
    public Material colorPalmDown;

    RotationDetectorSingle rotationDetector;

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
        //renderer.material = colorNormal;
        // Debug.Log("PA FistColorChange init : color2 set.");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        rotationDetector = leftHand.GetComponent<RotationDetectorSingle>();
        if (rotationDetector != null)
        {
            // Subscribe to the FistClosed event
            rotationDetector.palmUp_Fist += OnPalmUp_Fist;
            rotationDetector.palmDown_Fist += OnPalmDown_Fist;
            rotationDetector.palmNormal_Fist += OnpalmNormal_Fist;
            //Debug.Log("PA FistColorChange : subscription done.");
        }
    }

    private void OnPalmUp_Fist()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorPalmUp;
            Debug.Log("PA : Palm Up");
        }
    }

    private void OnPalmDown_Fist()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorPalmDown;
        }
    }

    private void OnpalmNormal_Fist()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorNormal;
        }
    }


    void Update()
    {

        
    }
}
