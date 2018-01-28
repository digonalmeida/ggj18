using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {
    public Text zombiesText;
    public Text savesText;

	public GameObject zombiePanel;

	public Image panelImage;
	// Use this for initialization
	void Start () {
		panelImage = zombiePanel.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
        int maxZombies = GameManager.Instance.maxZombie;
        int zombies = GameManager.Instance.contZombie;
        int saves = GameManager.Instance.scoreSaved;
        zombiesText.text = zombies.ToString() + "/" + maxZombies;
        savesText.text = saves.ToString() ;
	
		if (zombies >= maxZombies * 0.75) {
			panelImage.color = new Color32 (200, 0, 0, 184);
		} else if (zombies >= maxZombies * 0.50) {
			panelImage.color = new Color32 (255, 0, 0, 184);
		} else {
			panelImage.color = new Color32 (194, 194, 194, 184);
		}
	}
}
