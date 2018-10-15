using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallable : MonoBehaviour {
    Rigidbody myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody>();
	}

    public void Freeze()
    {
        myRigidbody.isKinematic = true;
    }

    public void Unfreeze()
    {
        myRigidbody.isKinematic = false;
    }

    public bool Stopped()
    {
        return myRigidbody.velocity == Vector3.zero;
    }
}
