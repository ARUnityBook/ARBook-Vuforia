/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;
using Vuforia;

public class ScanLine : MonoBehaviour
{
    #region PRIVATE_MEMBERS

    private float mTime = 0;
    private float mScanDuration = 4;//seconds
    private bool mMovingDown = true;

    #endregion //PRIVATE_MEMBERS


    #region PUBLIC_METHODS

    public void ResetAnimation()
    {
        mTime = 0;
        mMovingDown = true;
    }

    #endregion //PUBLIC_METHODS


    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTime = 0;
    }

    void Update()
    {
        float u = mTime / mScanDuration;
        mTime += Time.deltaTime;
        if (u > 1)
        {
            // invert direction
            mMovingDown = !mMovingDown;
            u = 0;
            mTime = 0;
        }

        // Get the main camera
        Camera cam = DigitalEyewearARController.Instance.PrimaryCamera ?? Camera.main;
        float viewAspect = cam.pixelWidth / (float)cam.pixelHeight;
        float fovY = Mathf.Deg2Rad * cam.fieldOfView;
        float depth = 1.02f * cam.nearClipPlane;
        float viewHeight = 2 * depth * Mathf.Tan(0.5f * fovY);
        float viewWidth = viewHeight * viewAspect;

        // Position the mesh
        float y = -0.5f * viewHeight + u * viewHeight;
        if (mMovingDown) y *= -1;
        
        this.transform.localPosition = new Vector3(0, y, depth);

        // Scale the quad mesh to fill the camera view
        float scaleX = 1.02f * viewWidth;
        float scaleY = scaleX / 32;
        this.transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
    }

    #endregion //UNITY_MONOBEHAVIOUR_METHODS
}
