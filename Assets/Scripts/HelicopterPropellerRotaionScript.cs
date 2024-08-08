using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPropellerRotationScript : MonoBehaviour
{
    public Transform upperPropellerTransform;
    public Transform tailPropellerTransform;

    float speed = 30.0f;

    float angleX = 0.0f;
    float angleY = 5.0f;
    float angleZ = 0.0f;

    float angleYT = 0.0f;
    float angleZT = 50.0f;

    // Variables for floating effect
    public float floatAmplitude = 10f; // Amplitude of the floating effect
    public float floatFrequency = 5.0f; // Frequency of the floating effect

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position; // Store the original position of the helicopter
    }

    // Update is called once per frame
    void Update()
    {
        // Propeller rotation
        tailPropellerTransform.Rotate(angleX, angleYT, -angleZT * speed * Time.deltaTime);
        upperPropellerTransform.Rotate(angleX, angleY, angleZ * speed * Time.deltaTime);

        // Floating effect
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
