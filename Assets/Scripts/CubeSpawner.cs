using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject player;

    private bool shouldMove = false;

    public GameObject victoryCubePrefab;
    public GameObject cubePrefab1; 
    public GameObject cubePrefab2; 
    public Vector3 spawnPosition;  
    public float spawnInterval = 2f; 
    public int cubesBeforeVictory = 5;

    private bool isCube1Next = true; 

    void Start()
    {
        StartCoroutine(SpawnCubes());
    }
    IEnumerator SpawnCubes()
    {
        int cubeCount = 0;

        while (cubeCount < cubesBeforeVictory) 
        {
            yield return new WaitForSeconds(spawnInterval);

            GameObject cubeToSpawn = isCube1Next ? cubePrefab1 : cubePrefab2;
            Instantiate(cubeToSpawn, spawnPosition, Quaternion.identity);

            isCube1Next = !isCube1Next;
            cubeCount++;
        }
        yield return new WaitForSeconds(spawnInterval);
        Instantiate(victoryCubePrefab, spawnPosition, Quaternion.identity);
    }
    public void StopMoving()
    {
        shouldMove = false;
    }
}
