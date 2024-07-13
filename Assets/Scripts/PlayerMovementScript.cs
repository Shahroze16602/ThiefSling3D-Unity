using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    Rigidbody playerRigidBody;
    public int playerSpeed = 5;
    public int jumpForce = 5;

    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
    }

    public void PlayerJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
