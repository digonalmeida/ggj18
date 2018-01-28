using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {
    public Text zombiesText;
    public Text savesText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int maxZombies = GameManager.Instance.maxZombie;
        int zombies = GameManager.Instance.contZombie;
        int saves = GameManager.Instance.scoreSaved;
        zombiesText.text = zombies.ToString() + "/" + maxZombies;
        savesText.text = saves.ToString() ;
	}
}
