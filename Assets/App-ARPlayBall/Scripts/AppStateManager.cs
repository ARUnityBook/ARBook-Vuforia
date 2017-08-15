using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vuforia;

public class AppStateManager : MonoBehaviour {

    [SerializeField]
    private AppStates appState;
    //Vuforia scripts that are used to get the state of the app. Can be found using FindObjectOfType<T>();
    public ReconstructionBehaviour reconstructionBehaviour;
    public SurfaceBehaviour surfaceBehaviour;
    public ImageTrackableEventHandler imageTarget;

    ///UI
    public GameObject instructionHolder;
    public Text instructionsText;
    public Button doneButton;
    public Button resetButton;

    //For the game
    public UnityEvent OnStartGame;

    //UI resources for instructions
    public string pointDeviceText;
    public string pullBackText;


    void Start() {
        imageTarget.OnImageTrackableFoundFirstTime.AddListener(OnImageTrackableFoundFirstTime);
    }

    void Update() {
        //We declare the bool values here because we want them to be set to false, unless the state is correct
        //This saves us from setting the values to false in each state.
        bool showDoneButton = false;
        bool showResetButton = false;

        switch (appState) {
            //Detection phase
            case AppStates.OVERLAY_OUTLINE:
                instructionsText.text = pointDeviceText;
                surfaceBehaviour.GetComponent<Renderer>().enabled = false;
                break;

            // The animation that is played when the trackable is found for the first time
            case AppStates.INIT_ANIMATION:
                appState = AppStates.SCANNING;
                break;

            // Scanning phase
            case AppStates.SCANNING:
                ShowWireFrame(true);
                instructionsText.text = pullBackText;
                showDoneButton = true;
                break;

            // When the user taps done. This happens before the game can be played
            case AppStates.GAME_RENDERING:
                if ((reconstructionBehaviour != null) && (reconstructionBehaviour.Reconstruction != null)) {
                    ShowWireFrame(false);
                    surfaceBehaviour.GetComponent<Renderer>().enabled = false;
                    imageTarget.ToggleOnStateChange = true;
                    reconstructionBehaviour.Reconstruction.Stop();
                    OnStartGame.Invoke();
                    appState = AppStates.GAME_PLAY;
                }
                break;

            //This is where the user can shoot the ball
            case AppStates.GAME_PLAY:
                instructionHolder.gameObject.SetActive(false);
                showResetButton = true;
                break;

            //User taps on [RESET] button - Re-loads the level
            case AppStates.RESET_ALL:
                //Reloads this scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                appState = AppStates.NONE;
                break;

            // Just a placeholder state, to make sure that the previous state runs for just one frame.
            case AppStates.NONE: break;
        }

        if (doneButton != null &&
            showDoneButton != doneButton.enabled) {
            doneButton.enabled = showDoneButton;
            doneButton.image.enabled = showDoneButton;
            doneButton.gameObject.SetActive(showDoneButton);
        }

        if (resetButton != null &&
            showResetButton != resetButton.enabled) {
            resetButton.enabled = showResetButton;
            resetButton.image.enabled = showResetButton;
            resetButton.gameObject.SetActive(showResetButton);
        }
    }

    private void OnImageTrackableFoundFirstTime() {
        appState = AppStates.INIT_ANIMATION;
    }

    void ShowWireFrame(bool show) {
        WireframeBehaviour[] wireframeBehaviours = FindObjectsOfType<WireframeBehaviour>();
        foreach (WireframeBehaviour wireframeBehaviour in wireframeBehaviours) {
            wireframeBehaviour.ShowLines = show;
        }
    }

    //Called by the buttons
    public void TerrainDone() {
        appState = AppStates.GAME_RENDERING;
    }

    public void ResetAll() {
        appState = AppStates.RESET_ALL;
    }

}
