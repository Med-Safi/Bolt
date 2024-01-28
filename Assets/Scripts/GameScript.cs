using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    public GameObject[] prefabs;
    public Vector3 initialSpawnPosition;
    public float spacing = 2f; 

    void Start()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            Vector3 spawnPosition = initialSpawnPosition + new Vector3(0, 0, i * spacing);
            Instantiate(prefabs[i], spawnPosition, Quaternion.identity);
        }
    }
}
