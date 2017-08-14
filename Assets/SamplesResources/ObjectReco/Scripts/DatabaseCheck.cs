/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class DatabaseCheck : MonoBehaviour
{
    public Canvas errorCanvas;
    public Text errorTitle;
    public Text errorMessage;

    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(FindDatasets);
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void CloseErrorDialog()
    {
        if (errorCanvas)
        {
            errorCanvas.enabled = false;
            errorCanvas.gameObject.SetActive(false);
            errorCanvas.transform.parent.position = 2 * Vector3.right * Screen.width;
        }
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    private void FindDatasets()
    {
        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (tracker == null)
        {
            Debug.LogError("ObjectTracker not initialized.");
            ShowError();
        }
        else
        {
            IEnumerable<DataSet> datasets = tracker.GetDataSets();
            if (datasets.Count() == 0)
            {
                Debug.LogError("Missing Database.");
                errorTitle.text = "Database not found";
                errorMessage.text = "Please scan an object and load a database.";
                ShowError();
            }
        }
    }

    private void ShowError()
    {
        if (errorCanvas)
        {
            errorCanvas.enabled = true;
            errorCanvas.gameObject.SetActive(true);
            errorCanvas.transform.parent.position = Vector3.zero;
        }
    }
    #endregion //PRIVATE_METHODS
}
