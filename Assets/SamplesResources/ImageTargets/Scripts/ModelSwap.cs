/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;


public class ModelSwap : MonoBehaviour 
{
    private GameObject mDefaultModel;
    private GameObject mExtTrackedModel;
    private GameObject mActiveModel = null;
    private TrackableSettings mTrackableSettings = null;

	void Start () 
    {
        mDefaultModel = this.transform.Find("teapot").gameObject;
        mExtTrackedModel = this.transform.Find("tower").gameObject;
        mActiveModel = mDefaultModel;
        mTrackableSettings = FindObjectOfType<TrackableSettings>();
    }
    
    void Update () 
    {
        if (mTrackableSettings.IsExtendedTrackingEnabled() && (mActiveModel == mDefaultModel))
        {
            // Switch 3D model to tower
            mDefaultModel.SetActive(false);
            mExtTrackedModel.SetActive(true);
            mActiveModel = mExtTrackedModel;
        }
        else if (!mTrackableSettings.IsExtendedTrackingEnabled() && (mActiveModel == mExtTrackedModel))
        {
            // Switch 3D model to teapot
            mExtTrackedModel.SetActive(false);
            mDefaultModel.SetActive(true);
            mActiveModel = mDefaultModel;
        }
    }
}
