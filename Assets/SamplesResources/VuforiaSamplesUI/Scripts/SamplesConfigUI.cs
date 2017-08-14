/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SamplesConfigUI : MonoBehaviour
{

    public enum UIPresetsEnum
    {
        ImageTargets = 0,
        VuMark = 1,
        CylinderTargets = 2,
        MultiTargets = 3,
        UserDefinedTargets = 4,
        ObjectReco = 5,
        CloudReco = 6,
        TextReco = 7,
        VirtualButtons = 8,
        SmartTerrain = 9
    }
    
    public UIPresetsEnum UIPresets;

    public Text SamplesTitle;

    public GameObject ExtendedTracking;
    public GameObject Autofocus;
    public GameObject Flash;
    public GameObject CameraGroup;
    public GameObject DatasetGroup;
    public GameObject VirtualButtonsGroup;
    public GameObject SmartTerrainGroup;

}
