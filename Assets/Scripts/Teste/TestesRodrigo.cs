using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD:Assets/Menu/MenuMannager.cs
public class MenuMannager : MonoBehaviour {

	public Text scoreText;
	public int score;
=======
public class TestesRodrigo : MonoBehaviour {
    float walkVelocity = 1;
>>>>>>> 7ae1742bee413d8a709a9b634713c34d339c6272:Assets/Scripts/Teste/TestesRodrigo.cs
	// Use this for initialization
	void Start () {
		this.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD:Assets/Menu/MenuMannager.cs
		score = (int) Time.timeSinceLevelLoad;
		scoreText.text = "" + score;
=======

        transform.position += (Vector3) Vector2.right * (walkVelocity * Time.deltaTime);
>>>>>>> 7ae1742bee413d8a709a9b634713c34d339c6272:Assets/Scripts/Teste/TestesRodrigo.cs
	}
}
