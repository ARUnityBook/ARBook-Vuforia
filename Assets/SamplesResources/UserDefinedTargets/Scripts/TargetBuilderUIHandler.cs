/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class TargetBuilderUIHandler : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    private Canvas mTargetBuilderCanvas;
    private MenuAnimator mMenuAnim;
    private bool mVuforiaStarted = false;
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start () 
    {
        mMenuAnim = FindObjectOfType<MenuAnimator>();
        mTargetBuilderCanvas = GetComponentInChildren<Canvas>();
        mTargetBuilderCanvas.enabled = false;
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
	}

    void Update()
    {
        if (mVuforiaStarted)
        {
            // The target builder canvas must be shown only when the menu UI is NOT shown,
            // and vice versa (so that they do not overlap each other)
            bool menuShown = mMenuAnim && mMenuAnim.IsVisible();
            if (mTargetBuilderCanvas.enabled == menuShown)
                mTargetBuilderCanvas.enabled = !menuShown;
        }
        else
        {
            if (mTargetBuilderCanvas.enabled)
                mTargetBuilderCanvas.enabled = false;
        }
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void OnVuforiaStarted()
    {
        // Vuforia has started successfully
        mVuforiaStarted = true;
    }
    #endregion //PRIVATE_METHODS
}
