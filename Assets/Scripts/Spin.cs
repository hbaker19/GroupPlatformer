using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().angularVelocity = 3000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
