using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject enemyFiery;
    private GameObject enemyCyclope;
    public int initialFierys = 5;
    public int initialCyclopes = 1;
    public int waves = 20;
    private int waveNumber;
    private int incrementFierys = 2;
    private int incrementCyclopes = 1;
    private int fierysSpawned;
    private int cyclopesSpawned;
    private bool waveComing;
    public float timeBetweenEnemies = 4f;
    public float timeBetweenWaves = 2f;
    public float countdown = 3f;

    private void Start() {
        enemyFiery = (GameObject)Resources.Load("Prefabs/Monsters/Fiery", typeof(GameObject));
        enemyCyclope = (GameObject)Resources.Load("Prefabs/Monsters/Cyclope", typeof(GameObject));
        waveNumber = 0;
        fierysSpawned = 0;
        cyclopesSpawned = 0;
        waveComing = true;
    }

    private void Update() {
        if(!waveComing && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && waveNumber < waves -1) {
            Debug.Log(waveNumber);
            fierysSpawned = 0;
            cyclopesSpawned = 0;
            waveComing = true;
            countdown = timeBetweenWaves;
            waveNumber++;
        } else if (countdown <= 0f) {
            SpawnWave();
            countdown = timeBetweenEnemies;
        }
        countdown -= Time.deltaTime;
    }

    private void SpawnWave() {
        waveComing = false;
        if (fierysSpawned < (initialFierys + (incrementFierys * waveNumber))) {
            SpawnFiery();
        } else if(cyclopesSpawned < (initialCyclopes + (incrementCyclopes * waveNumber))) {
            SpawnCyclope();
        }
    }

    void SpawnFiery() {
        Instantiate(enemyFiery.transform, transform.position, transform.rotation);
        fierysSpawned++;
    }

    private void SpawnCyclope() {
        Instantiate(enemyCyclope.transform, transform.position, transform.rotation);
        cyclopesSpawned++;
    }
}
