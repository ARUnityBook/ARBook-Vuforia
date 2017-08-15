using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetView : MonoBehaviour {
    public Transform solarSystem;
    public Transform planet;

    void Update() {
        Vector3 position = solarSystem.localPosition;
        if (planet != null) {
            // move solar system so planet is in the center
            position.x = -planet.localPosition.x;
            position.z = -planet.localPosition.z;
        } else {
            // center solar system on the sun
            position.x = 0f;
            position.z = 0f;
        }
        solarSystem.localPosition = position;
    }
}
