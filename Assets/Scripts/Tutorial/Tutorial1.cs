using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour {
    public GameObject zombie;
    Paciente paciente;
    public GameObject tutorial2;
    SafeZone safeZone;
    GameController gameController;
    public Collider2D bounds;
    public void Close()
    {
        safeZone.enabled = true ;
        gameObject.SetActive(false);
    }

    public void Start()
    {
        safeZone = FindObjectOfType<SafeZone>();
        safeZone.enabled = false;
        paciente = zombie.GetComponent<Paciente>();
        paciente.setState(Paciente.State.DUMMY);
        zombie.GetComponent<Animator>().Play("idle");
        gameController = FindObjectOfType<GameController>();
        gameController.mouseBounds = bounds;

    }

	
	// Update is called once per frame
	void Update () {
		if(zombie.transform.position.y < 1 && paciente.state == Paciente.State.HOSPITAL)
        {
            Close();
            tutorial2.SetActive(true);
        }
	}
}
