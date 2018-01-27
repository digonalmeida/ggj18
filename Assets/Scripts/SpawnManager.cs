using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public float spawnDelay;
    public Transform[] spawnPoints;
    public GameObject pacienteGO;
    public Vector2 randomness;
    void Start () {
        InvokeRepeating("SpawnGO", 1, spawnDelay);
	}
	
	void Update () {
		
	}

    void SpawnGO ()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
        spawnPosition.y += Random.Range(-1.3f, 0.2f);
        Instantiate(pacienteGO, spawnPosition, spawnPoints[spawnPointIndex].rotation);
    }
}
