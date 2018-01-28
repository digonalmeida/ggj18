using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : MonoBehaviour {
    public Collider2D bounds;

    public Paciente[] pacientes;
    public GameObject tutorial4;
    public GameObject highlight;
    GameController gameController;
    SafeZone safeZone;
    bool missionCompleted = false;
	// Use this for initialization
	void Start () {
        
        safeZone = FindObjectOfType<SafeZone>();
        safeZone.enabled = false;
        gameController = FindObjectOfType<GameController>();
        gameController.mouseBounds = bounds;
	}
	void Close()
    {
        tutorial4.SetActive(true);
        gameObject.SetActive(false);
        safeZone.enabled = false;
    }
	// Update is called once per frame
	void Update () {
		if(missionCompleted)
        {
            int countState = 0;
            for(int i = 0; i < 3; i++)
            {
                if (pacientes[i].state != Paciente.State.HOSPITAL)
                {
                    countState++;
                    if(countState >= 2)
                    {
                        return;
                    }
                }
            }

            Close();

            return;
        }

        if(!missionCompleted)
        {
            if (gameController.holding >= 3)
            {
                missionCompleted = true;
                highlight.SetActive(false);
            }
        }
	}
}
