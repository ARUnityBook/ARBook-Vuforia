using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class ImageTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {

    public UnityEvent OnImageTrackableFoundFirstTime;
    private bool toggleOnStateChange;

    private TrackableBehaviour mTrackableBehaviour;
    private bool m_TrackableDetectedForFirstTime;

    void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour) {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public bool ToggleOnStateChange {
        get { return toggleOnStateChange; }
        set { toggleOnStateChange = value; ToggleComponenets(value); }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus) {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED) {
            OnTrackingFound();
        } else {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound() {
        if (toggleOnStateChange)
            ToggleComponenets(true);
        if (!m_TrackableDetectedForFirstTime) {
            OnImageTrackableFoundFirstTime.Invoke();
            m_TrackableDetectedForFirstTime = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    private void OnTrackingLost() {
        if (toggleOnStateChange)
            ToggleComponenets(false);

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    void ToggleComponenets(bool enabled) {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
        Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);
        // Enable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = enabled;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents) {
            component.enabled = enabled;
        }

        //Enable Canvases
        foreach (Canvas component in canvasComponents) {
            component.enabled = enabled;
        }
    }
}
