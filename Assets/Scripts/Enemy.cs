using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    VisualMapGenerator mappyBoi;

    void Start()
    {
        mappyBoi = GetComponentInParent<VisualMapGenerator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillPlane"))
            Destroy(gameObject);
        else
        {
            Blocks collidedBlockType = other.GetComponent<BlockInfo>().blockType;

            if (collidedBlockType == Blocks.Goal)
            {
                mappyBoi.GenerateMap();
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
                Destroy(gameObject);
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
