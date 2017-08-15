using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

[RequireComponent(typeof(TrackableBehaviour))]
public class TrackableObjectVisibility : MonoBehaviour, ITrackableEventHandler {

    public UnityEvent OnTargetFound;
    public UnityEvent OnTargetLost;

    private TrackableBehaviour trackableBehaviour;

    void Start() {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {

        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            OnTargetFound.Invoke();
        } else {
            OnTargetLost.Invoke();
        }
    }

}
