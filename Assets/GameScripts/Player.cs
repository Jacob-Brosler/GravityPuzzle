using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    VisualMapGenerator mappyBoi;
    public Rigidbody MyRigidbody;
    public int playerID;
    public bool enemy = false;

    void Awake()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        mappyBoi = GetComponentInParent<VisualMapGenerator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        mappyBoi.falling = false;
        if (!enemy && collision.transform.GetComponent<Player>() != null && collision.transform.GetComponent<Player>().enemy)
            mappyBoi.GenerateMap();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillPlane"))
            mappyBoi.GenerateMap();
        else
            mappyBoi.CheckTriggerer(other.transform.GetComponent<BlockInfo>().arrayPosition.x, other.transform.GetComponent<BlockInfo>().arrayPosition.y, playerID, enemy);
    }

    public void TurnToEnemy()
    {
        enemy = true;
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
