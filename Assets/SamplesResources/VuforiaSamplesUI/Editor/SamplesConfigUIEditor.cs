/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SamplesConfigUI))]
[CanEditMultipleObjects]
public class SamplesConfigUIEditor : Editor
{

    private SamplesConfigUI _myTarget;

    #region UNITY_EDITOR_METHODS

    private void OnEnable()
    {
        _myTarget = (SamplesConfigUI)target;
        
        ToggleUIFeatures();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        ToggleUIFeatures();
    }

    #endregion // UNITY_EDITOR_METHODS

    #region PRIVATE_METHODS

    private void ToggleUIFeatures()
    {
        switch (_myTarget.UIPresets)
        {
        case(SamplesConfigUI.UIPresetsEnum.ImageTargets):
            _myTarget.SamplesTitle.text = "Image Targets";
            SetUIOptions(true, true, true, true, true, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.VuMark):
            _myTarget.SamplesTitle.text = "VuMark";
            SetUIOptions(true, false, false, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.CylinderTargets):
            _myTarget.SamplesTitle.text = "Cylinder Targets";
            SetUIOptions(false, false, false, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.MultiTargets):
            _myTarget.SamplesTitle.text = "Multi Targets";
            SetUIOptions(false, false, false, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.UserDefinedTargets):
            _myTarget.SamplesTitle.text = "User Defined Targets";
            SetUIOptions(true, false, false, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.ObjectReco):
            _myTarget.SamplesTitle.text = "Object Reco";
            SetUIOptions(true, false, true, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.CloudReco):
            _myTarget.SamplesTitle.text = "Cloud Reco";
            SetUIOptions(true, false, false, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.TextReco):
            _myTarget.SamplesTitle.text = "Text Reco";
            SetUIOptions(false, false, false, false, false, false, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.VirtualButtons):
            _myTarget.SamplesTitle.text = "Virtual Buttons";
            SetUIOptions(false, false, false, false, false, true, false);
            break;
        case(SamplesConfigUI.UIPresetsEnum.SmartTerrain):
            _myTarget.SamplesTitle.text = "Smart Terrain";
            SetUIOptions(false, false, false, false, false, false, true);
            break;
        }
    }

    private void SetUIOptions(bool ET, bool AF, bool Z, bool Cam, bool DS, bool VB, bool ST)
    {
        _myTarget.ExtendedTracking.SetActive(ET);
        _myTarget.Autofocus.SetActive(AF);
        _myTarget.Flash.SetActive(Z);
        _myTarget.CameraGroup.SetActive(Cam);
        _myTarget.DatasetGroup.SetActive(DS);
        _myTarget.VirtualButtonsGroup.SetActive(VB);
        _myTarget.SmartTerrainGroup.SetActive(ST);
    }

    #endregion // PRIVATE_METHODS


}
