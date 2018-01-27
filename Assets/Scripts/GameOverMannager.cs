using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMannager : MonoBehaviour {

	public Text score;

    AudioManagerSingleton.AudioClipName gameOverSfx = AudioManagerSingleton.AudioClipName.GAMEOVER;

    // Use this for initialization
    void Start () {
        AudioManagerSingleton.instance.StopSound(AudioManagerSingleton.AudioType.MUSIC);
        AudioManagerSingleton.instance.PlaySound(gameOverSfx, AudioManagerSingleton.AudioType.SFX);
        score.text = "Score: " + GameScoreMannager.Instance.scoreSaved;
		Debug.Log("In game Over: " + GameScoreMannager.Instance.scoreSaved);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
