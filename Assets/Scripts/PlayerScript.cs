using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ISlowMotionCallBacks
{
    [SerializeField] private float playerSpeed = 15f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float minForce = 10f;
    [SerializeField] private float maxHorizontalAngle = 15f;
    [SerializeField] private Transform trajectoryLineStartPosition;
    [SerializeField] private TrajectoryLineScript trajectoryLineScript;
    [SerializeField] private SlowMotionHandler slowMotionHandler;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float cameraMoveDistance = 0.1f;
    [SerializeField] private CoinsScript coinsScript;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float landingAnimationThreshold = 0.5f;
    [SerializeField] private GameObject tutorialUI;

    private Vector3 touchStartPosition;
    private Rigidbody playerRigidbody;
    private bool isGrounded = false;
    private bool isDragging = false;
    private bool isChecking = true;
    private bool isLanding = false;

    private bool tutorialShown = false;
    private PlayerAnimationController playerAnimationController;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        slowMotionHandler.SetSlowMotionCallbacks(this);
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        playerAnimationController.SetRunning(true);
        tutorialShown = false;
    }

    private void FixedUpdate()
    {
        CheckGroundStatus();

        if (isGrounded && transform.position.y > -1)
        {
            MoveForward();
        }
        if (transform.position.y < 1)
        {
            playerAnimationController.SetFalling(true);
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
        if (isGrounded)
        {
            playerRigidbody.velocity = new Vector3(currentVelocity.x, currentVelocity.y, forwardVelocity.z);
        }
    }

    private void OnTouchDown(Vector3 touchPosition)
    {
        if (isGrounded && slowMotionHandler.IsInSlowMo)
        {
            touchStartPosition = touchPosition;
            isDragging = true;

            if (tutorialUI != null)
            {
                tutorialUI.SetActive(false);
            }
        }
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

            if (force.magnitude >= minForce)
            {
                StartCoroutine(PerformJumpWithDelay(force));
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

        float angle = Mathf.Atan2(forceDirection.x, forceDirection.y) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -maxHorizontalAngle, maxHorizontalAngle);
        forceDirection.x = Mathf.Tan(angle * Mathf.Deg2Rad) * forceDirection.y;

        return forceDirection * forceMagnitude;
    }

    private IEnumerator PerformJumpWithDelay(Vector3 force)
    {
        playerAnimationController.TriggerJump();
        trajectoryLineScript.HideTrajectoryLine();
        slowMotionHandler.StopSlowMo();
        isChecking = false;
        yield return new WaitForSeconds(0.0f);
        isGrounded = false;
        playerRigidbody.velocity = Vector3.zero;
        ApplyJumpForce(force);
    }

    private void ApplyJumpForce(Vector3 force)
    {
        playerRigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void CheckGroundStatus()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        Debug.DrawRay(transform.position, Vector3.down * landingAnimationThreshold, Color.green);

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, landingAnimationThreshold, groundLayer))
        {
            if (!isLanding)
            {
                isLanding = true;
                playerAnimationController.SetLanded(true);
            }
        }
        else
        {
            if (isLanding)
            {
                isLanding = false;
                playerAnimationController.SetLanded(false);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            if (isChecking)
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                    playerRigidbody.velocity = Vector3.zero;
                    playerAnimationController.SetLanded(true);
                }
            }
        }
        else
        {
            if (isChecking)
            {
                if (isGrounded)
                {
                    isGrounded = false;
                }
            }
            else
            {
                isChecking = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0;
            gameManager.ShowLevelFailedUI();
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

        if (!tutorialShown && tutorialUI != null)
        {
            tutorialUI.SetActive(true);
            tutorialShown = true;
        }
    }

    void ISlowMotionCallBacks.OnSlowMotionEnd()
    {
        cameraMovement.MoveBack(Vector3.zero, 0.5f);
    }
}
