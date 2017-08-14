/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;

public class SamplesNavigationHandler : MonoBehaviour
{
    
    #region PRIVATE_MEMBERS
    
    private string currentSceneName;
    
    #endregion // PRIVATE_MEMBERS
    
    #region PUBLIC_METHODS
    
    public void OnStartAR()
    {
        // called by OK button on About screen
        SamplesMainMenu.LoadScene(SamplesMainMenu.LoadingScene);
    }
    
    public void LoadMenuScene()
    {
        // called by "Vuforia Samples" button in AR scene UI menu
        // also called below in Update() if Android Back button pressed
        SamplesMainMenu.LoadScene(SamplesMainMenu.MenuScene);
    }
    
    #endregion // PUBLIC_METHODS
    
    #region MONOBEHAVIOUR_METHODS
    
    void Start()
    {
        #if (UNITY_5_2 || UNITY_5_1 || UNITY_5_0)
        currentSceneName = Application.loadedLevelName;
        #else // UNITY_5_3 or above
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        #endif
    }
    
    void Update()
    {
        #if (UNITY_EDITOR || UNITY_ANDROID)
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            
            Debug.Log("Esc/Back button pressed from " + currentSceneName);
            
            if (currentSceneName == "Vuforia-1-Menu")
            {
                if (SamplesMainMenu.isAboutScreenVisible)
                {
                    gameObject.GetComponent<SamplesMainMenu>().BackToMenu();
                }
                else
                {
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #elif UNITY_ANDROID
                    // On Android, the Back button is mapped to the Esc key
                    Application.Quit();
                    #endif
                }
            }
            else
            {
                LoadMenuScene();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            
            if (currentSceneName == "Vuforia-1-Menu" && SamplesMainMenu.isAboutScreenVisible)
            {
                // In Unity 'Return' key same as clicking OK button on About Screen
                // On ODG R7, JoystickButton0 is the Trackpad select button
                OnStartAR();
            }
        }
        
        #endif // UNITY_EDITOR || UNITY_ANDROID
    }
    
    #endregion // MONOBEHAVIOUR_METHODS
    
}