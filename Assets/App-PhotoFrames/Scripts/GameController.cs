using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject defaultPictureObject;
    public Transform imageTarget;

    private int delay = 0;
    public static GameController instance;
    private AudioSource clickSound;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        clickSound = GetComponent<AudioSource>();
    }

    void Update() {
        if (delay == 0 && GameObject.FindGameObjectsWithTag("Picture").Length == 0) {
            
            CreateNewPicture();
        }
        if (++delay > 30) delay = 0;
    }


    public void CreateNewPicture() {
        GameObject newPicture = Instantiate(defaultPictureObject, imageTarget);
    }

    public void PlayClickFeedback() {
        if (clickSound != null) {
            clickSound.Play();
        }
    }

}
