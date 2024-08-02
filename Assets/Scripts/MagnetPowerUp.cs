using UnityEngine;
using System.Collections.Generic;

public class MagnetPowerUp : MonoBehaviour
{
    public float magnetRange = 10.0f;
    public float magnetForce = 10.0f;
    public float magnetDuration = 5.0f;
    private bool isMagnetActive = false;
    private float magnetTimer;

    private float tapTimeWindow = 0.3f; // Maximum time between taps for double-tap
    private float lastTapTime = 0f;

    void Update()
    {
        DetectDoubleTap();

        if (isMagnetActive)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer <= 0)
            {
                isMagnetActive = false;
            }
            else
            {
                AttractCoins();
            }
        }
    }

    void DetectDoubleTap()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < tapTimeWindow)
            {
                ActivateMagnet();
            }
            lastTapTime = currentTime;
        }

        // For testing in the editor using the mouse
        if (Input.GetMouseButtonDown(0))
        {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < tapTimeWindow)
            {
                ActivateMagnet();
            }
            lastTapTime = currentTime;
        }
    }

    public void ActivateMagnet()
    {
        isMagnetActive = true;
        magnetTimer = magnetDuration;
        Debug.Log("Magnet Activated");
    }

    void AttractCoins()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Coin"))
            {
                Rigidbody coinRb = hitCollider.GetComponent<Rigidbody>();
                if (coinRb != null)
                {
                    Vector3 direction = (transform.position - hitCollider.transform.position).normalized;
                    coinRb.velocity = direction * magnetForce;
                    Debug.Log("Coin Attracted: " + hitCollider.gameObject.name);
                }
                else
                {
                    Debug.LogWarning("Coin is missing Rigidbody: " + hitCollider.gameObject.name);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnetRange);
    }
}
