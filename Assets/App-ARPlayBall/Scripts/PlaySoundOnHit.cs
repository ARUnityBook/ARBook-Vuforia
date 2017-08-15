using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnHit : MonoBehaviour {

    public AudioClip clip;
    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
        source.spatialBlend = 1.0f;
        source.playOnAwake = false;
        source.clip = clip;
    }

    void OnCollisionEnter()  //Plays Sound Whenever collision detected
    {
        source.Play();
    }

}