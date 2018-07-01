using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    VisualMapGenerator mappyBoi;
    public Rigidbody MyRigidbody;

    void Awake()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        mappyBoi = GetComponentInParent<VisualMapGenerator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        mappyBoi.falling = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillPlane"))
            mappyBoi.GenerateMap();
        else
            mappyBoi.CheckTriggerer(other.transform.GetComponent<BlockInfo>().arrayPosition.x, other.transform.GetComponent<BlockInfo>().arrayPosition.y);
    }

    public void Stop()
    {
        MyRigidbody.isKinematic = true;
    }

    public void CanMove()
    {
        MyRigidbody.isKinematic = false;
    }
}
