using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    VisualMapGenerator mappyBoi;
    Rigidbody MyRigidbody;

    void Awake()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        mappyBoi = GetComponentInParent<VisualMapGenerator>();
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (mappyBoi.moving == 0 && mappyBoi.falling)
        {
            //Debug.DrawLine(transform.position + Vector3.down * 0.5f, transform.position + Vector3.down * 0.6f, Color.red, 0);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.51f))
            {
                mappyBoi.CheckSpace(int.Parse(hit.transform.gameObject.name.Substring(0, hit.transform.gameObject.name.IndexOf(" "))), int.Parse(hit.transform.gameObject.name.Substring(hit.transform.gameObject.name.IndexOf(" ") + 1)));
            }
            else
            {
                mappyBoi.falling = true;
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        mappyBoi.CheckSpace(int.Parse(collision.transform.gameObject.name.Substring(0, collision.transform.gameObject.name.IndexOf(" "))), int.Parse(collision.transform.gameObject.name.Substring(collision.transform.gameObject.name.IndexOf(" ") + 1)));
    }

    public void Stop()
    {
        MyRigidbody.useGravity = false;
        MyRigidbody.velocity = Vector3.zero;
    }

    public void CanMove()
    {
        MyRigidbody.useGravity = true;
    }
}
