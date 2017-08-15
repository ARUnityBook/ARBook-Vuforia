using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowControl : MonoBehaviour {
    public float ballStartZ = 0.5f;

    public Vector2 sensivity = new Vector2(8f, 100f);
    public float speed = 5f;
    public float resetBallAfterSeconds = 3f;

    public UnityEvent OnReset;

    private Vector3 direction;

    private Vector3 newBallPosition;
    private Rigidbody _rigidbody;
    private bool isHolding;
    private bool isThrown;
    private bool isInitialized = false;

    private Vector3 inputPositionCurrent;
    private Vector2 inputPositionPivot;
    private Vector2 inputPositionDifference;

    private RaycastHit raycastHit;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        ReadyBall();
        isInitialized = true;
    }

    void Update() {
        bool isInputBegan = false;
        bool isInputEnded = false;
#if UNITY_EDITOR
        isInputBegan = Input.GetMouseButtonDown(0);
        isInputEnded = Input.GetMouseButtonUp(0);
        inputPositionCurrent = Input.mousePosition;
#else
	isInputBegan = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began;
	isInputEnded = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended;
	inputPositionCurrent = Input.GetTouch (0).position;
#endif
        if (isHolding)
            OnTouch();

        if (isThrown)
            return;

        if (isInputBegan) {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(inputPositionCurrent), out raycastHit, 100f)) {
                if (raycastHit.transform == transform) {
                    isHolding = true;
                    transform.SetParent(null);
                    inputPositionPivot = inputPositionCurrent;
                }
            }
        }

        if (isInputEnded) {
            if (inputPositionPivot.y < inputPositionCurrent.y) {
                Throw(inputPositionCurrent);
            }
        }
    }

    void Throw(Vector2 inputPosition) {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.useGravity = true;

        inputPositionDifference.y = (inputPosition.y - inputPositionPivot.y) / Screen.height * sensivity.y;

        inputPositionDifference.x = (inputPosition.x - inputPositionPivot.x) / Screen.width;
        inputPositionDifference.x =
            Mathf.Abs(inputPosition.x - inputPositionPivot.x) / Screen.width * sensivity.x * inputPositionDifference.x;

        direction = new Vector3(inputPositionDifference.x, 0f, 1f);
        direction = Camera.main.transform.TransformDirection(direction);

        _rigidbody.AddForce((direction + Vector3.up) * speed * inputPositionDifference.y);

        isHolding = false;
        isThrown = true;

        if (_rigidbody)
            Invoke("ReadyBall", resetBallAfterSeconds);
    }



    void ReadyBall() {
        CancelInvoke();

        Vector3 screenPosition = new Vector3(0.5f, 0.1f, ballStartZ);

        transform.position = Camera.main.ViewportToWorldPoint(screenPosition);

        newBallPosition = transform.position;
        isThrown = isHolding = false;

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        //_rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        transform.rotation = Quaternion.Euler(0f, 200f, 0f);
        transform.SetParent(Camera.main.transform);

        if (isInitialized)
            OnReset.Invoke();
    }

    void OnTouch() {
        inputPositionCurrent.z = ballStartZ;
        newBallPosition = Camera.main.ScreenToWorldPoint(inputPositionCurrent);
        transform.localPosition = newBallPosition;
    }

}
