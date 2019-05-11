using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawnManager : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetAvailibleSpawnpoint(int playerNum)
    {
        Vector3 spawnPos = new Vector3();

        int spawnIndex = Random.Range(0, spawnPoints.Count);
        bool validSpawn = spawnPoints[spawnIndex].IsSafeToSpawn();
        while (!validSpawn)
        {
            spawnIndex = Random.Range(0, spawnPoints.Count);
            validSpawn = spawnPoints[spawnIndex].IsSafeToSpawn();
        }


        spawnPos = spawnPoints[spawnIndex].transform.position;
        return spawnPos;
    }
}
