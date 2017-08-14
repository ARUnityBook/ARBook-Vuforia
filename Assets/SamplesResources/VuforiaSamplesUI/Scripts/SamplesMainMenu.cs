/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class SamplesMainMenu : MonoBehaviour
{

	#region PUBLIC_MEMBERS

    public enum MenuItem
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

    public Canvas AboutCanvas;
    public Text AboutTitle;
    public Text AboutDescription;

    public static bool isAboutScreenVisible = false;

    // initialize static enum with one of the items
    public static MenuItem menuItem = MenuItem.ImageTargets;

    public const string MenuScene = "Vuforia-1-Menu";
    public const string LoadingScene = "Vuforia-2-Loading";

    SamplesAboutScreenInfo aboutScreenInfo;

	#endregion // PUBLIC_MEMBERS

	#region MONOBEHAVIOUR_METHODS

    void Start()
    {
        // reset about screen state variable to false when returning from AR scene
        isAboutScreenVisible = false;

        if (aboutScreenInfo == null)
        {
            // initialize if null
            aboutScreenInfo = new SamplesAboutScreenInfo();
        }
    }

	#endregion // MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS

    public static string GetSceneToLoad()
    {
        // called by SamplesLoadingScreen to load selected AR scene
        return "Vuforia-3-" + SamplesMainMenu.menuItem.ToString();
    }

    public static void LoadScene(string scene)
    {
        #if (UNITY_5_2 || UNITY_5_1 || UNITY_5_0)
        Application.LoadLevel(scene);
        #else // UNITY_5_3 or above
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        #endif
    }

    public void BackToMenu()
    {
        // called to return to Menu from About screen
        AboutCanvas.sortingOrder = -1;
        SamplesMainMenu.isAboutScreenVisible = false;
    }

    public void LoadAboutScene(string itemSelected)
    {
        UpdateConfiguration(itemSelected);

        // This method called from list of Sample App menu buttons
        switch (itemSelected)
        {

        case("ImageTargets"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.ImageTargets;
            break;
        case("VuMark"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.VuMark;
            break;
        case("CylinderTargets"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.CylinderTargets;
            break;
        case("MultiTargets"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.MultiTargets;
            break;
        case("UserDefinedTargets"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.UserDefinedTargets;
            break;
        case("ObjectReco"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.ObjectReco;
            break;
        case("CloudReco"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.CloudReco;
            break;
        case("TextReco"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.TextReco;
            break;
        case("VirtualButtons"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.VirtualButtons;
            break;
        case("SmartTerrain"):
            SamplesMainMenu.menuItem = SamplesMainMenu.MenuItem.SmartTerrain;
            break;
        }

        AboutTitle.text = aboutScreenInfo.GetTitle(SamplesMainMenu.menuItem.ToString());
        AboutDescription.text = aboutScreenInfo.GetDescription(SamplesMainMenu.menuItem.ToString());

        AboutCanvas.transform.parent.transform.position = Vector3.zero; // move canvas into position
        AboutCanvas.sortingOrder = 1; // bring canvas in front of main menu
        isAboutScreenVisible = true;

    }

    public void UpdateConfiguration(string scene)
    {

        var config = VuforiaConfiguration.Instance;
        var dbConfig = config.DatabaseLoad;
        var stConfig = config.SmartTerrainTracker;

        // all settings which are changed for a scene, have to be reset here
        // because any change is persistent throughout the whole application
        dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new string[0];
        stConfig.AutoInitAndStartTracker = stConfig.AutoInitBuilder = false;
        config.Vuforia.MaxSimultaneousImageTargets = 1;

        switch (scene)
        {
            case ("ImageTargets"):
                dbConfig.DataSetsToLoad = new[] {"StonesAndChips", "Tarmac"};
                dbConfig.DataSetsToActivate = new[] { "StonesAndChips" };
                break;
            case ("VuMark"):
                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] {"Vuforia"};
                config.Vuforia.MaxSimultaneousImageTargets = 10;
                break;
            case ("CylinderTargets"):
                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] {"sodacan"};
                break;
            case ("MultiTargets"):
                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] {"FlakesBox"};
                break;
            case ("VirtualButtons"):
                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] {"StonesAndWood"};
                break;
            case ("SmartTerrain"):
                stConfig.AutoInitAndStartTracker = stConfig.AutoInitBuilder = true;
                dbConfig.DataSetsToLoad = dbConfig.DataSetsToActivate = new[] {"StonesAndChips"};
                break;
        }
    }

	#endregion // PUBLIC_METHODS

}
