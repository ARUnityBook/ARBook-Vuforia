using UnityEngine;

public class TimedDisable : MonoBehaviour {

    public float time = 4f;

    void OnEnable() {
        Invoke("Disable", time);
    }

    void Disable() {
        gameObject.SetActive(false);
    }
}
