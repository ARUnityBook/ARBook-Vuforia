using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayAnimationOnClick : MonoBehaviour
{
    [SerializeField]
    private string TriggerName;
    private Animator currentAnimator;

	// Use this for initialization
	void Start ()
	{
	    currentAnimator = GetComponent<Animator>();


	}

    void OnMouseDown()
    {
        currentAnimator.SetTrigger(TriggerName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
