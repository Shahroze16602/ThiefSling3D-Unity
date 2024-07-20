using UnityEngine;

public class PlayerScript : MonoBehaviour, ISlowMotionCallBacks
{
    [SerializeField] private float playerSpeed = 15f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private Transform trajectoryLineStartPosition;
    [SerializeField] private TrajectoryLineScript trajectoryLineScript;
    [SerializeField] private SlowMotionHandler slowMotionHandler;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private float cameraMoveDistance = 0.1f;

    private Vector3 mouseDownPosition;
    private Rigidbody playerRigidbody;
    private bool isGrounded = false;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        slowMotionHandler.SetSlowMotionCallbacks(this);
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        Vector3 forwardVelocity = transform.forward * playerSpeed;
        Vector3 currentVelocity = playerRigidbody.velocity;
        playerRigidbody.velocity = new Vector3(currentVelocity.x, currentVelocity.y, forwardVelocity.z);
    }

    private void OnMouseDown()
    {
        mouseDownPosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (isGrounded && slowMotionHandler.IsInSlowMo)
        {
            DrawTrajectoryLine();
        }
    }

    private void OnMouseUp()
    {
        if (isGrounded && slowMotionHandler.IsInSlowMo)
        {
            Vector3 dragDistance = mouseDownPosition - Input.mousePosition;
            Vector3 force = CalculateJumpForce(dragDistance);
            Jump(force);
        }
    }

    private void DrawTrajectoryLine()
    {
        Vector3 dragDistance = mouseDownPosition - Input.mousePosition;
        Vector3 force = CalculateJumpForce(dragDistance);
        trajectoryLineScript.ShowTrajectoryLine(trajectoryLineStartPosition.position, force);
    }

    private Vector3 CalculateJumpForce(Vector3 dragDistance)
    {
        const float dragFactor = 10f;

        float dragDistanceMagnitude = dragDistance.magnitude / dragFactor;
        Vector3 dragDirection = dragDistance.normalized;
        float forceMagnitude = Mathf.Clamp(dragDistanceMagnitude, 0, maxForce);

        Vector3 forceDirection = new Vector3(dragDirection.x, Mathf.Abs(dragDirection.y), Mathf.Abs(dragDirection.y)).normalized;

        return forceDirection * forceMagnitude;
    }

    private void Jump(Vector3 force)
    {
        if (isGrounded)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(force, ForceMode.Impulse);
            trajectoryLineScript.HideTrajectoryLine();
            slowMotionHandler.StopSlowMo();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerRigidbody.velocity = Vector3.zero;
            isGrounded = true;
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
    }

    // Callbacks for SlowMotionHandler
    void ISlowMotionCallBacks.OnSlowMotionStart()
    {
        cameraMovement.MoveRight(cameraMoveDistance, 0.075f);
    }

    void ISlowMotionCallBacks.OnSlowMotionEnd()
    {
        cameraMovement.MoveBack(Vector3.zero, 0.5f);
    }
}
