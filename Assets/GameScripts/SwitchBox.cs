using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBox : MonoBehaviour {

    public bool open = false;

	public void ToggleState()
    {
        open = !open;
        if (open)
            gameObject.layer = 12;
        else
            gameObject.layer = 1;
    }
}
