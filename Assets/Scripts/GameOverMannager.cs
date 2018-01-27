using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMannager : MonoBehaviour {

	public Text score;
	// Use this for initialization
	void Start () {
		score.text = "Score: " + GameScoreMannager.Instance.scoreSaved;
		Debug.Log("In game Over: " + GameScoreMannager.Instance.scoreSaved);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
