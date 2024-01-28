using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public static float speed = -35f;
    public static float destroyZPosition = -60f; // Z-coordinate threshold for destruction

    void Update()
    {
        // Move the object forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the object's z-coordinate is less than or equal to the destroyZPosition
        if (transform.position.z <= destroyZPosition)
        {
            Destroy(gameObject);
        }
    }
}
