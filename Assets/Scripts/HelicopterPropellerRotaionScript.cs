using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPropellerRotaionScript : MonoBehaviour
{
    public Transform upperPropellerTransform;
    public Transform tailPropellerTransform;

    float speed = 30.0f;

    float angleX = 0.0f;
    float angleY = 5.0f;
    float angleZ = 0.0f;

    float angleYT = 0.0f;
    float angleZT = 50.0f;


    // Update is called once per frame
    void Update()
    {

        tailPropellerTransform.Rotate(angleX, angleYT, -angleZT * speed * Time.deltaTime);

        upperPropellerTransform.Rotate(angleX, angleY, angleZ * speed * Time.deltaTime);

    }
}
