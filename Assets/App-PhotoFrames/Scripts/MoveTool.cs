using UnityEngine;

public class MoveTool : MonoBehaviour {
    public LayerMask WallLayerMask;

    private bool isEditing;
    private Vector3 originaButtonScale;
    private BoxCollider collider;
    private Vector3 originColliderSize;

    private PictureController picture;
    private Vector3 relativeOffset;

    void Start() {
        isEditing = false;
        originaButtonScale = transform.localScale;
        collider = GetComponent<BoxCollider>();
        originColliderSize = collider.size;
        picture = GetComponentInParent<PictureController>();
        relativeOffset = transform.position - picture.transform.position;
        relativeOffset.y = 0f;
    }

    void Update() {
        if (isEditing) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, WallLayerMask)) {
                Debug.DrawLine(ray.origin, hit.point);
                picture.transform.position = hit.point - relativeOffset;
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

    private void OnMouseUp() {
        if (isEditing) {
            DoneEdit();
        }
    }

    private void BeginEdit() {
        if (!isEditing) {
            isEditing = true;
            transform.localScale = originaButtonScale * 2.5f;
            collider.size = Vector3.one;
        }
    }

    private void DoneEdit() {
        if (isEditing) {
            isEditing = false;
            transform.localScale = originaButtonScale;
            collider.size = originColliderSize;
        }
    }

}
