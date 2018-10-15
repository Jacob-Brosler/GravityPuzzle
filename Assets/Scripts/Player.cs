using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    VisualMapGenerator mappyBoi;

    void Start()
    {
        mappyBoi = GetComponentInParent<VisualMapGenerator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Enemy>() != null)
            mappyBoi.GenerateMap();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("KillPlane"))
            mappyBoi.GenerateMap();
        else
        {
            Blocks collidedBlockType = other.GetComponent<BlockInfo>().blockType;

            if (collidedBlockType == Blocks.Goal)
            {
                mappyBoi.playerCount--;
                mappyBoi.CheckForLevelCompletion();
                Destroy(gameObject);
            }
            else if (collidedBlockType == Blocks.Teleporter)
            {
                if (other.GetComponent<Toggleable>().toggleState)
                {
                    transform.position = other.GetComponent<LinkedBlock>().linkedBlock.transform.position;
                    other.GetComponent<Toggleable>().ToggleState();
                    other.GetComponent<LinkedBlock>().linkedBlock.GetComponent<Toggleable>().ToggleState();
                }
            }
            else if (collidedBlockType == Blocks.Spike)
            {
                mappyBoi.GenerateMap();
            }
            else if (collidedBlockType == Blocks.Switch)
            {
                Debug.Log("toggling state");
                other.GetComponent<LinkedBlock>().linkedBlock.GetComponent<Toggleable>().ToggleState();
                other.GetComponent<LinkedBlock>().linkedBlock.gameObject.SetActive(other.GetComponent<LinkedBlock>().linkedBlock.GetComponent<Toggleable>().toggleState);
            }
        }
    }
}
