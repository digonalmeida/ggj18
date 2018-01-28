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
		AudioManagerSingleton.instance.PlaySound (
			AudioManagerSingleton.AudioClipName.HOSPITAL, AudioManagerSingleton.AudioType.MUSIC, true, 1f);
		GameManager.Instance.cleanGame ();
	}
	
	void Update () {
		
	}

    void SpawnGO ()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
        spawnPosition.y += Random.Range(-0.5f, 0.5f);
        Instantiate(pacienteGO, spawnPosition, spawnPoints[spawnPointIndex].rotation);
		GameManager.Instance.contZombie++;
		GameManager.Instance.checkMaxZombie ();
    }
}
