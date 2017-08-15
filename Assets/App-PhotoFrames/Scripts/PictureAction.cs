using UnityEngine;

public class PictureAction : MonoBehaviour {
    public PictureCommand command;

    protected PictureController picture;
    protected Animator animator;

    void Start() {
        picture = GetComponentInParent<PictureController>();
        animator = GetComponent<Animator>();
    }

    void OnMouseDown() {
        if (animator != null) {
            animator.SetTrigger("Click");
        }
        GameController.instance.PlayClickFeedback();
        Invoke("DoExecute", 1);
    }

    void DoExecute() {
        picture.Execute(command);
    }
}