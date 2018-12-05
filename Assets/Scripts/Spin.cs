using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float spin = 1000;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().angularVelocity = spin;
    }
	
	// Update is called once per frame
	void Update ()
    {
    }
}
