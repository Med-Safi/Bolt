using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCubeMove : MonoBehaviour
{
    private bool shouldMove = true; // Flag to control movement
    public float moveSpeed = 5f; // Speed of the victory cube

    void Update()
    {
        if (shouldMove)
        {
            // Example movement logic
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) return;
        if (other.CompareTag("Player"))
        {
            shouldMove = false;
        }
    }
}
