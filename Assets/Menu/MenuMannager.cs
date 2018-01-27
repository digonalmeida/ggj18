using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMannager : MonoBehaviour {

	public Text scoreText;
	public int score;
	// Use this for initialization
	void Start () {
		this.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		score = (int) Time.timeSinceLevelLoad;
		scoreText.text = "" + score;
	}
}
