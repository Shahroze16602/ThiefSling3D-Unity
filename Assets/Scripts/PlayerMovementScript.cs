using UnityEngine;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour
{
    Rigidbody playerRigidBody;
    public float playerSpeed = 5f;
    public float maxJumpForce = 20f;
    public float minDragDistance = 0.1f;
    public float maxDragDistance = 2f;

    public Image ringImage;
    public Image circleImage;

    private Vector3 initialTouchPosition;
    private bool isDragging = false;
    private bool isGrounded = false;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        ringImage.gameObject.SetActive(false);
        circleImage.gameObject.SetActive(false);
    }

    void Update()
    {
<<<<<<< Updated upstream
        MovePlayer();
        HandleSlingshotJump();
    }

    void MovePlayer()
    {
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
=======
        PlayerJump();
>>>>>>> Stashed changes
    }

    void HandleSlingshotJump()
    {
<<<<<<< Updated upstream
        if (isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
                StartDrag(Input.mousePosition);

            if (Input.GetMouseButton(0) && isDragging)
                UpdateDrag(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && isDragging)
                EndDrag(Input.mousePosition);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                    StartDrag(touch.position);

                if (touch.phase == TouchPhase.Moved && isDragging)
                    UpdateDrag(touch.position);

                if (touch.phase == TouchPhase.Ended && isDragging)
                    EndDrag(touch.position);
            }
        }
    }

    void StartDrag(Vector3 startPosition)
    {
        initialTouchPosition = startPosition;
        isDragging = true;
        ringImage.gameObject.SetActive(true);
        circleImage.gameObject.SetActive(true);
        ringImage.transform.position = initialTouchPosition;
        circleImage.transform.position = initialTouchPosition;
    }

    void UpdateDrag(Vector3 currentTouchPosition)
    {
        Vector3 dragVector = currentTouchPosition - initialTouchPosition;
        float maxDragDistanceInPixels = maxDragDistance * Screen.dpi;
        Vector3 constrainedPosition = initialTouchPosition + Vector3.ClampMagnitude(dragVector, maxDragDistanceInPixels);
        circleImage.transform.position = constrainedPosition;
    }

    void EndDrag(Vector3 releaseTouchPosition)
    {
        isDragging = false;
        Vector3 dragVector = releaseTouchPosition - initialTouchPosition;
        float dragDistance = dragVector.magnitude / Screen.dpi;

        if (dragDistance > minDragDistance)
        {
            dragDistance = Mathf.Clamp(dragDistance, 0, maxDragDistance);
            float jumpForce = (dragDistance / maxDragDistance) * maxJumpForce;

            if (isGrounded)
            {
                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, jumpForce, playerRigidBody.velocity.z);
                isGrounded = false;
            }
        }

        ringImage.gameObject.SetActive(false);
        circleImage.gameObject.SetActive(false);
=======
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
>>>>>>> Stashed changes
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0, playerRigidBody.velocity.z);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
