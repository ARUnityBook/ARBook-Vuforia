using UnityEngine;

public class ScaleTool : MonoBehaviour {
    public LayerMask WallLayerMask;

    private PictureController picture;

    private bool isEditing = false;

    private Vector3 originaButtonScale;

    //Used to calculate the mouse position
    private Vector3 startPosition = Vector3.zero;
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 initialScale = Vector3.zero;

    void Start() {
        picture = GetComponentInParent<PictureController>();
        originaButtonScale = transform.localScale;
    }

    void Update() {
        if (isEditing) {
            currentPosition = Input.mousePosition;
            float difference = (currentPosition - startPosition).magnitude;
            //Scaling down is possible by dragging your mouse to the left.
            int direction = currentPosition.x > startPosition.x ? 1 : -1;
            float scaleFactor = 1 + (difference / Screen.width) * direction;
            if (scaleFactor > 0.1f) {
                picture.transform.localScale = initialScale * scaleFactor;
            }
        }
        if (!Input.GetMouseButton(0)) {
            DoneEdit();
        }
    }

    private void OnMouseDown() {
        if (!isEditing) {
            BeginEdit();
        }
    }

    public void BeginEdit() {
        if (!isEditing) {
            isEditing = true;

            transform.localScale = originaButtonScale * 2.5f;
            startPosition = Input.mousePosition;
            initialScale = picture.transform.localScale;
        }
    }

    private void OnMouseUp() {
        if (isEditing) {
            DoneEdit();
        }
    }

    public void DoneEdit() {
        if (isEditing) {
            isEditing = false;
            transform.localScale = originaButtonScale;
        }
    }
}
