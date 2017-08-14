/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Vuforia;


public class VBMenuOptions : MenuOptions
{
    #region PUBLIC_MEMBER_VARIABLES
    public Material mVirtualButtonMaterial;
    #endregion PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBERS
    private ImageTargetBehaviour mImageTargetWood = null;

    private Dictionary<string, Vector3> mVBPositionDict =
        new Dictionary<string, Vector3>();

    // Dictionary for storing virtual button scale values.
    private Dictionary<string, Vector3> mVBScaleDict =
        new Dictionary<string, Vector3>();
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    protected override void Start()
    {
        base.Start();
        VuforiaARController.Instance.RegisterVuforiaInitializedCallback(OnVuforiaInitialized);
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Create or destroy the virtual button with the given name.
    /// </summary>
    public void ToggleVirtualButton(string name)
    {
        if (mImageTargetWood.ImageTarget != null)
        {
            // Get the virtual button if it exists.
            VirtualButton vb = mImageTargetWood.ImageTarget.GetVirtualButtonByName(name);

            if (vb != null)
            {
                // Destroy the virtual button if it exists.
                mImageTargetWood.DestroyVirtualButton(name);
            }
            else
            {
                // Get the position and scale originally used for this virtual button.
                Vector3 position, scale;
                if (mVBPositionDict.TryGetValue(name, out position) &&
                    mVBScaleDict.TryGetValue(name, out scale))
                {
                    // Deactivate the dataset before creating the virtual button.
                    ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                    DataSet dataSet = objectTracker.GetActiveDataSets().First();
                    objectTracker.DeactivateDataSet(dataSet);

                    // Create the virtual button.
                    VirtualButtonBehaviour vbb = mImageTargetWood.CreateVirtualButton(
                        name,
                        new Vector2(position.x, position.z),
                        new Vector2(scale.x, scale.z)) as VirtualButtonBehaviour;

                    if (vbb != null)
                    {
                        // Register the button with the event handler on the Wood target.
                        vbb.RegisterEventHandler(mImageTargetWood.GetComponent<VirtualButtonEventHandler>());

                        // Add a mesh to outline the button.
                        CreateVBMesh(vbb);

                        // If the Wood target isn't currently tracked hide the button.
                        if (mImageTargetWood.CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
                        {
                            vbb.GetComponent<Renderer>().enabled = false;
                        }
                    }

                    // Reactivate the dataset.
                    objectTracker.ActivateDataSet(dataSet);
                }
            }
        }
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    private void OnVuforiaInitialized()
    {
        // Find the Wood image target.
        mImageTargetWood = GameObject.Find("ImageTargetWood").GetComponent<ImageTargetBehaviour>();

        // Add a mesh for each virtual button on the Wood target.
        VirtualButtonBehaviour[] vbs =
                mImageTargetWood.gameObject.GetComponentsInChildren<VirtualButtonBehaviour>();
        foreach (VirtualButtonBehaviour vb in vbs)
        {
            CreateVBMesh(vb);
            // Also store the position and scale for later.
            mVBPositionDict.Add(vb.VirtualButtonName, vb.transform.localPosition);
            mVBScaleDict.Add(vb.VirtualButtonName, vb.transform.localScale);
        }
    }

    /// <summary>
    /// Create a mesh outline for the virtual button.
    /// </summary>
    private void CreateVBMesh(VirtualButtonBehaviour vb)
    {
        GameObject vbObject = vb.gameObject;

        MeshFilter meshFilter = vbObject.GetComponent<MeshFilter>();
        if (!meshFilter)
        {
            meshFilter = vbObject.AddComponent<MeshFilter>();
        }

        // Setup vertex positions.
        Vector3 p0 = new Vector3(-0.5f, 0, -0.5f);
        Vector3 p1 = new Vector3(-0.5f, 0, 0.5f);
        Vector3 p2 = new Vector3(0.5f, 0, -0.5f);
        Vector3 p3 = new Vector3(0.5f, 0, 0.5f);

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
        mesh.triangles = new int[]  { 0,1,2,   2,1,3 };

        // Add UV coordinates.
        mesh.uv = new Vector2[]{
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
                };

        // Add empty normals array.
        mesh.normals = new Vector3[mesh.vertices.Length];

        // Automatically calculate normals.
        mesh.RecalculateNormals();
        mesh.name = "VBPlane";

        meshFilter.sharedMesh = mesh;

        MeshRenderer meshRenderer = vbObject.GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            meshRenderer = vbObject.AddComponent<MeshRenderer>();
        }

        meshRenderer.sharedMaterial = mVirtualButtonMaterial;
    }
    #endregion //PRIVATE_METHODS
}
