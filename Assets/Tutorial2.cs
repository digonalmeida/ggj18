using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour {
    GameController gameController;
    public Collider2D bounds;
    public GameObject pacienteObject;
    public GameObject tutorial3;
    Paciente paciente;
	// Use this for initialization
	void Start () {
        paciente = pacienteObject.GetComponent<Paciente>();
        gameController = FindObjectOfType<GameController>();
        gameController.mouseBounds = bounds;
    }
	
	// Update is called once per frame
	void Update () {
        paciente.infectionLevel = 0;
		if(pacienteObject == null)
        {
            tutorial3.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
