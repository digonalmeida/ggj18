using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int holding = 0;
    public int maxHolding = 3;
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
}
