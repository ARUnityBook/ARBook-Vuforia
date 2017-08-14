/*==============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.

Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.  
==============================================================================*/
using UnityEditor;
using UnityEngine;
using Vuforia;

/// <summary>
/// This editor class renders the custom inspector for the CloudRecoEventHandler MonoBehaviour
/// </summary>
[CustomEditor(typeof(CloudRecoEventHandler))]
public class CloudRecoEventHandlerEditor : Editor
{
    #region UNITY_EDITOR_METHODS

    /// <summary>
    /// Draws a custom UI for the cloud reco event handler inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        // record potential changes for this object
        Undo.RecordObject(target, "Inspector Change");

        CloudRecoEventHandler creh = (CloudRecoEventHandler)target;

        EditorGUILayout.HelpBox("Here you can set the ImageTargetBehaviour from the scene that will be used to augment new cloud reco search results.", MessageType.Info);
        bool allowSceneObjects = !EditorUtility.IsPersistent(target);
        creh.ImageTargetTemplate = (ImageTargetBehaviour)EditorGUILayout.ObjectField("Image Target Template",
                                                    creh.ImageTargetTemplate, typeof(ImageTargetBehaviour), allowSceneObjects);

        creh.scanLine = (ScanLine)EditorGUILayout.ObjectField("Scan Line",
                                                    creh.scanLine, typeof(ScanLine), true);

        creh.cloudErrorCanvas = (UnityEngine.Canvas)EditorGUILayout.ObjectField("Cloud Reco Error Canvas",
                                                    creh.cloudErrorCanvas, typeof(UnityEngine.Canvas), true);

        creh.cloudErrorTitle = (UnityEngine.UI.Text)EditorGUILayout.ObjectField("Cloud Reco Error Title",
                                                    creh.cloudErrorTitle, typeof(UnityEngine.UI.Text), true);

        creh.cloudErrorText = (UnityEngine.UI.Text)EditorGUILayout.ObjectField("Cloud Reco Error Text",
                                                    creh.cloudErrorText, typeof(UnityEngine.UI.Text), true);


        if (GUI.changed)
            EditorUtility.SetDirty(creh);
    }

    #endregion // UNITY_EDITOR_METHODS
}
