using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int scoreSaved = 0;

	public int maxZombie = 20;

	public int contZombie = 0;

	public int highScore = 0;

	public bool gameOverInfected = false;

	private static string keyScore = "HighScore";

	public static GameManager mInstance;

	public static GameManager Instance
	{
		get
		{
			if (mInstance == null){
				GameObject go = new GameObject();
				mInstance = go.AddComponent<GameManager>();
				mInstance.scoreSaved = 0;
				mInstance.highScore = PlayerPrefs.GetInt (keyScore, 0);
			}
			return mInstance;
		}
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void cleanGame(){
		if (scoreSaved > highScore) {
			highScore = scoreSaved;
			PlayerPrefs.SetInt (keyScore, highScore);
		}
		scoreSaved = 0;

		contZombie = 0;

	}

	public void checkMaxZombie(){
		if (contZombie >= maxZombie) {
			SceneManager.LoadScene("GameOver");
			gameOverInfected = false;
		}
	}
}
