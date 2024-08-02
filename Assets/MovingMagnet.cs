using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMagnet : MonoBehaviour
{
    public Transform cubeTransform;

    public bool isMagnetActivated;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cubeTransform.position, 2.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMagnetActivated = true;
            Destroy(gameObject);
        }
    }
}
