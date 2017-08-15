using UnityEngine;
using UnityEngine.Events;

public class GameObjectEvent : UnityEvent<GameObject> {
}

public class CollisionBehavior : MonoBehaviour {
    public GameObjectEvent OnHitGameObject = new GameObjectEvent();
    public UnityEvent OnCollision = new UnityEvent();

    // used to make sure that only one event is called
    private GameObject lastCollision;

    void Awake() {
        //disables the renderer in playmode if it wasn't already
        MeshRenderer targetMeshRenderer = GetComponent<MeshRenderer>();
        if (targetMeshRenderer != null)
            targetMeshRenderer.enabled = false;
    }

    void OnCollisionEnter(Collision collision) {
        if (lastCollision != collision.gameObject) {
            OnHitGameObject.Invoke(collision.gameObject);
            OnCollision.Invoke();
            lastCollision = collision.gameObject;
        }
    }

    //So that the goal can be a trigger
    void OnTriggerEnter(Collider collider) {
        if (lastCollision != collider.gameObject) {
            OnHitGameObject.Invoke(collider.gameObject);
            OnCollision.Invoke();
            lastCollision = collider.gameObject;
        }
    }

    public void ResetCollision() {
        lastCollision = null;
    }

    void OnDisable() {
        lastCollision = null;
    }
}
