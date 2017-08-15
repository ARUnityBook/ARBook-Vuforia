using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterControls : MonoBehaviour {
    public float gametimePerDay = 0.05f;

    public void SlowTime() {
        gametimePerDay = 24f;
    }

    public void ResetTime() {
        gametimePerDay = 0.05f;
    }
}
