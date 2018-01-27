using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestesRodrigo : MonoBehaviour {
    float walkVelocity = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += (Vector3) Vector2.right * (walkVelocity * Time.deltaTime);
	}
}
