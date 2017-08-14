/*==============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Copyright (c) 2015 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class STMenuOptions : MenuOptions
{
    #region PRVATE_MEMBERS
    private ReconstructionBehaviour mReconstructionBehaviour;
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    protected override void Start()
    {
        base.Start();
        mReconstructionBehaviour = FindObjectOfType<ReconstructionBehaviour>();
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void ToggleScanning()
    {
        if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null))
        {
            // swap UI button label (start vs. stop)
            Button startBtn = base.FindUISelectableWithText<Button>("Start");
            Button stopBtn = base.FindUISelectableWithText<Button>("Stop");
            if (startBtn)
            {
                mReconstructionBehaviour.Reconstruction.Start();
                startBtn.GetComponentInChildren<Text>().text = "Stop";
            }
            else if (stopBtn)
            {
                mReconstructionBehaviour.Reconstruction.Stop(); 
                stopBtn.GetComponentInChildren<Text>().text = "Start";
            }
        }
    }

    public void ResetSmartTerrain()
    {
        if ((mReconstructionBehaviour != null) && (mReconstructionBehaviour.Reconstruction != null))
        {
            Debug.Log("Resetting Smart Terrain");

            SmartTerrainTracker stTracker = TrackerManager.Instance.GetTracker<SmartTerrainTracker>();
            bool trackerWasActive = stTracker.IsActive;

            // first stop the tracker
            if (trackerWasActive)
                stTracker.Stop();

            // now you can reset...
            mReconstructionBehaviour.Reconstruction.Reset();
            
            // ... and restart the tracker
            if (trackerWasActive)
            {
                stTracker.Start();
                mReconstructionBehaviour.Reconstruction.Start();
            }

            // After a Reset, the ST reconstruction will be active again,
            // so we need to sync the UI
            Button startBtn = base.FindUISelectableWithText<Button>("Start");
            if (startBtn)
            {
                startBtn.GetComponentInChildren<Text>().text = "Stop";
            }
        }
    }
    #endregion //PUBLIC_METHODS
}
