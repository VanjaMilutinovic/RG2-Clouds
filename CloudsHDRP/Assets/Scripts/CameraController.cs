using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 30f;
    public float speedChangeAmount = 10f;
    public float minMoveSpeed = 5f;
    public float maxMoveSpeed = 200f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 60f;
    
    [Header("Zoom Settings")]
    public float zoomSpeed = 15f;
    public float minFov = 20f;
    public float maxFov = 90f;

    // Rotation variables
    private float _yaw;
    private float _pitch;

    /*
     * Reference to the Camera component for FOV zoom
     * FOV - Field Of View
     */
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        if (_mainCamera == null)
        {
            Debug.LogError("CameraController requires a Camera component to be attached to the same GameObject.");
            this.enabled = false; // Disable script if no camera is found
            return;
        }

        Vector3 startEuler = transform.eulerAngles;
        _yaw = startEuler.y;
        _pitch = startEuler.x;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleSpeedControl();
        HandleZoom();
    }

    private void HandleRotation()
    {
        // Get input from arrow keys for rotation ONLY
        float yawInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) yawInput = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) yawInput = 1f;
        
        float pitchInput = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) pitchInput = -1f;
        if (Input.GetKey(KeyCode.DownArrow)) pitchInput = 1f;

        _yaw += yawInput * rotationSpeed * Time.deltaTime;
        _pitch += pitchInput * rotationSpeed * Time.deltaTime;

        // Clamp the pitch to prevent the camera from flipping over
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);

        transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        // Forward/Backward movement
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }
        
        // Left/Right (Strafe) movement
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        // Up/Down movement
        if (Input.GetKey(KeyCode.E))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveDirection += Vector3.down;
        }
        
        // Apply movement to the camera's position
        // .normalized ensures consistent speed even when moving diagonally
        transform.position += moveDirection.normalized * (moveSpeed * Time.deltaTime);
    }

    private void HandleSpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            moveSpeed += speedChangeAmount;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            moveSpeed -= speedChangeAmount;
        }

        // Clamp the move speed within the defined min/max range
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    private void HandleZoom()
    {
        float scrollInput = Input.mouseScrollDelta.y;
        
        if (scrollInput != 0)
        {
            float newFov = _mainCamera.fieldOfView - scrollInput * zoomSpeed;
            _mainCamera.fieldOfView = Mathf.Clamp(newFov, minFov, maxFov);
        }
    }
}