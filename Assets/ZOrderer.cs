using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOrderer : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.z = pos.y * 10;
        transform.position = pos;
	}
}
