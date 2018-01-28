using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public int holding = 0;
    public int maxHolding = 3;
    public Collider2D mouseBounds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButton(0))
        {
            holding = 0;
        }

	}

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartGame()
    {

    }
}
