/*==============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2013-2015 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
==============================================================================*/
using UnityEngine;
using Vuforia;

/// <summary>
///  A custom handler that implements the ITrackerEventHandler interface.
/// </summary>
public class STEventHandler : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    private ReconstructionBehaviour mReconstructionBehaviour;
    #endregion //PRIVATE_MEMBERS


    #region PUBLIC_MEMBERS
    public PropBehaviour PropTemplate;
    public SurfaceBehaviour SurfaceTemplate;
    #endregion //PUBLIC_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        mReconstructionBehaviour = GetComponent<ReconstructionBehaviour>();
        if (mReconstructionBehaviour)
        {
            mReconstructionBehaviour.RegisterPropCreatedCallback(OnPropCreated);
            mReconstructionBehaviour.RegisterSurfaceCreatedCallback(OnSurfaceCreated);
        }
    }

    void OnDestroy()
    {
        if (mReconstructionBehaviour)
        {
            mReconstructionBehaviour.UnregisterPropCreatedCallback(OnPropCreated);
            mReconstructionBehaviour.UnregisterSurfaceCreatedCallback(OnSurfaceCreated);
        }
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void OnSurfaceCreated(Surface surface)
    {
        Debug.Log("---Created Surface");

        if (mReconstructionBehaviour != null)
            mReconstructionBehaviour.AssociateSurface(SurfaceTemplate, surface);
    }

    public void OnPropCreated(Prop prop)
    {
        Debug.Log("---Created Prop");

        if (mReconstructionBehaviour != null)
            mReconstructionBehaviour.AssociateProp(PropTemplate, prop);
    }
    #endregion //PUBLIC_METHODS
}



