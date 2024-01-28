using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public DrawingManager drawingManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Debug.Log("collect");
            if (drawingManager != null)
            {
                drawingManager.CollectPrefab();
            }
            else
            {
                Debug.LogError("DrawingManager is not assigned in the PlayerScript");
            }

            other.gameObject.SetActive(false);
        }

        else if (other.CompareTag("DestructiveObstacle"))
        {
            Debug.Log("DestructiveObstacle");
            if (drawingManager != null)
            {
                drawingManager.DestroyPrefab();
            }
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}