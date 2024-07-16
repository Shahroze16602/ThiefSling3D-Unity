using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private TrajectoryLineScript trajectoryLineScript;
    Rigidbody playerRigidBody;
    public float playerSpeed = 5f;
    public float jumpForce = 30;
    private Vector3 jumpForceDirection;
    [SerializeField]
    private bool isGrounded = false;

    private Vector3 jumpStartPoint;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        jumpForceDirection = (transform.forward + Vector3.up).normalized;
    }

    void Update()
    {
        if (isGrounded)
        {
            DrawTrajectoryLine();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ApplyForwardForceAtAngle();
        }
    }

    void ApplyForwardForceAtAngle()
    {
        jumpStartPoint = transform.position;
        Vector3 force = jumpForceDirection * jumpForce;
        playerRigidBody.AddForce(force, ForceMode.Impulse);
        trajectoryLineScript.HideTrajectoryLine();
    }

    private void DrawTrajectoryLine()
    {
        trajectoryLineScript.ShowTrajectoryLine(transform.position, jumpForceDirection * (jumpForce / playerRigidBody.mass));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
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
}