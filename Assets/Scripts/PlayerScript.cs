using UnityEngine;

public class PlayerScript : MonoBehaviour, ISlowMotionCallBacks
{
    [SerializeField] private float playerSpeed = 15f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float minForce = 10f; // Added minForce
    [SerializeField] private float maxHorizontalAngle = 15f; // Maximum angle for horizontal force
    [SerializeField] private Transform trajectoryLineStartPosition;
    [SerializeField] private TrajectoryLineScript trajectoryLineScript;
    [SerializeField] private SlowMotionHandler slowMotionHandler;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float cameraMoveDistance = 0.1f;

    private Vector3 touchStartPosition;
    private Rigidbody playerRigidbody;
    private bool isGrounded = false;
    private bool isDragging = false;


    [SerializeField] private CoinsScript coinsScript;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        slowMotionHandler.SetSlowMotionCallbacks(this);
    }

    private void FixedUpdate()
    {
        if (isGrounded && transform.position.y > -1)
        {
            MoveForward();
        }
        if (transform.position.y < -10)
        {
            Time.timeScale = 0;
            gameManager.ShowLevelFailedUI();
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);
        }
    }

    private void HandleTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                OnTouchDown(touch.position);
                break;
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                OnTouchDrag(touch.position);
                break;
            case TouchPhase.Ended:
                OnTouchUp(touch.position);
                break;
        }
    }

    private void MoveForward()
    {
        Vector3 forwardVelocity = transform.forward * playerSpeed;
        Vector3 currentVelocity = playerRigidbody.velocity;
        playerRigidbody.velocity = new Vector3(currentVelocity.x, currentVelocity.y, forwardVelocity.z);
    }

    private void OnTouchDown(Vector3 touchPosition)
    {
        touchStartPosition = touchPosition;
        isDragging = true;
    }

    private void OnTouchDrag(Vector3 touchPosition)
    {
        if (isGrounded && slowMotionHandler.IsInSlowMo && isDragging)
        {
            DrawTrajectoryLine(touchPosition);
        }
    }

    private void OnTouchUp(Vector3 touchPosition)
    {
        if (isGrounded && slowMotionHandler.IsInSlowMo && isDragging)
        {
            Vector3 dragDistance = touchStartPosition - touchPosition;
            Vector3 force = CalculateJumpForce(dragDistance);

            if (force.magnitude >= minForce) // Apply minForce restriction
            {
                Jump(force);
            }

            isDragging = false;
        }
    }

    private void DrawTrajectoryLine(Vector3 touchPosition)
    {
        Vector3 dragDistance = touchStartPosition - touchPosition;
        Vector3 force = CalculateJumpForce(dragDistance);
        trajectoryLineScript.ShowTrajectoryLine(trajectoryLineStartPosition.position, force);
    }

    private Vector3 CalculateJumpForce(Vector3 dragDistance)
    {
        const float dragFactor = 10f;

        float dragDistanceMagnitude = dragDistance.magnitude / dragFactor;
        Vector3 dragDirection = dragDistance.normalized;
        float forceMagnitude = Mathf.Clamp(dragDistanceMagnitude, minForce, maxForce);

        Vector3 forceDirection = new Vector3(dragDirection.x, Mathf.Abs(dragDirection.y), Mathf.Abs(dragDirection.y)).normalized;

        // Clamp the horizontal angle
        float angle = Mathf.Atan2(forceDirection.x, forceDirection.y) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -maxHorizontalAngle, maxHorizontalAngle);
        forceDirection.x = Mathf.Tan(angle * Mathf.Deg2Rad) * forceDirection.y;

        return forceDirection * forceMagnitude;
    }

    private void Jump(Vector3 force)
    {
        if (isGrounded)
        {
            playerRigidbody.velocity = Vector3.zero;
            isGrounded = false;
            playerRigidbody.AddForce(force, ForceMode.Impulse);
            trajectoryLineScript.HideTrajectoryLine();
            slowMotionHandler.StopSlowMo();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerRigidbody.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0;
            gameManager.ShowLevelFailedUI();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SlowMoTrigger"))
        {
            slowMotionHandler.StartSlowMotion();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            gameManager.ShowLevelCompleteUI();
        }
    }

    void ISlowMotionCallBacks.OnSlowMotionStart()
    {
        cameraMovement.MoveRight(cameraMoveDistance, 0.075f);
    }

    void ISlowMotionCallBacks.OnSlowMotionEnd()
    {
        cameraMovement.MoveBack(Vector3.zero, 0.5f);
    }
}
