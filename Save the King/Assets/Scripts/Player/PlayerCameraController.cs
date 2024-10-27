using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    public CinemachineFreeLook playerCamera;
    
    // Define your max/min limits for camera rotation
    public float maxYAxisAngle = 70f;  // Maximum vertical angle (Y-axis)
    public float minYAxisAngle = -10f; // Minimum vertical angle (Y-axis)
    public float maxXAxisRotation = 90f; // Maximum horizontal angle (X-axis)
    public float minXAxisRotation = -90f; // Minimum horizontal angle (X-axis)

    void Update()
    {
        // Get the current X and Y axis values from the Cinemachine camera
        float currentYAxis = playerCamera.m_YAxis.Value * 100;  // Y-axis is a normalized value (0 to 1)
        float currentXAxis = playerCamera.m_XAxis.Value;  // X-axis is in degrees 
        
        // Clamp the Y axis (vertical) rotation between the set limits
        playerCamera.m_YAxis.Value = Mathf.Clamp(currentYAxis, minYAxisAngle, maxYAxisAngle) / 100;

        // Clamp the X axis (horizontal) rotation between the set limits
        playerCamera.m_XAxis.Value = Mathf.Clamp(currentXAxis, minXAxisRotation, maxXAxisRotation);
    }
}
