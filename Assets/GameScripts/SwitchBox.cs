using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBox : MonoBehaviour {

    public bool open = false;

	public void ToggleState()
    {
        open = !open;
        if (open)
        {
            gameObject.SetActive(false);//.layer = 12;
        }
        else
        {
            gameObject.SetActive(true);//.layer = 1;
        }
    }
}
