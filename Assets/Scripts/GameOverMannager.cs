using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMannager : MonoBehaviour {

	public Text score;

	public Text gameOverText;

	public Text highScore;

	private string gameOverInfected = "You saved a zombie!";

	private string gameOverCityInfected = "Your city was infected!!!";

    AudioManagerSingleton.AudioClipName gameOverSfx = AudioManagerSingleton.AudioClipName.GAMEOVER;

    // Use this for initialization
    void Start () {
        AudioManagerSingleton.instance.StopSound(AudioManagerSingleton.AudioType.MUSIC);
        AudioManagerSingleton.instance.PlaySound(gameOverSfx, AudioManagerSingleton.AudioType.SFX);
		score.text = "Score: " + GameManager.Instance.scoreSaved;
		gameOverText.text = ( GameManager.Instance.gameOverInfected ? gameOverInfected : gameOverCityInfected );
		GameManager.Instance.cleanGame ();
		highScore.text = "Highscore: " + GameManager.Instance.highScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
