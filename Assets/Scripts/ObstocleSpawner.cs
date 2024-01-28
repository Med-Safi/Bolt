using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstocleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector3 leftPosition;
    public Vector3 middlePosition;
    public Vector3 rightPosition;
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomObstaclePattern();
        }
    }

    void SpawnRandomObstaclePattern()
    {
        bool spawnLeft = Random.value > 0.5f;
        bool spawnMiddle = Random.value > 0.5f;
        bool spawnRight = Random.value > 0.5f;

        if (spawnLeft)
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], leftPosition, Quaternion.identity);
        if (spawnMiddle)
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], middlePosition, Quaternion.identity);
        if (spawnRight)
            Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], rightPosition, Quaternion.identity);
    }
}
