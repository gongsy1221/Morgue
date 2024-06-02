using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoSingleton<KeySpawner>
{
    public GameObject objectToSpawn;
    public Transform[] spawnPoints;

    public void RandomSpawnKey()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
