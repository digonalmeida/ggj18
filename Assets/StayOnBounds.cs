using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnBounds : MonoBehaviour {
    GameController gameController;
	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController == null)
        {
            return;
        }
        if(gameController.mouseBounds == null)
        {
            return;
        }

        Vector3 pos = transform.position;
        if (pos.x > gameController.mouseBounds.bounds.max.x)
        {
            pos.x = gameController.mouseBounds.bounds.max.x;
        }
        if (pos.x < gameController.mouseBounds.bounds.min.x)
        {
            pos.x = gameController.mouseBounds.bounds.min.x;
        }
        if (pos.y > gameController.mouseBounds.bounds.max.y)
        {
            pos.y = gameController.mouseBounds.bounds.max.y;
        }
        if (pos.y < gameController.mouseBounds.bounds.min.y)
        {
            pos.y = gameController.mouseBounds.bounds.min.y;
        }
        
        transform.position = pos;
	}
}
