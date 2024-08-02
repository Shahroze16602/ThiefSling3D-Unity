using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingByMagnet : MonoBehaviour
{
    public Transform cube1Transform;

    float timer = 5.0f;

    public MovingMagnet movingMagnet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (movingMagnet.isMagnetActivated && timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, cube1Transform.position, 1.5f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
