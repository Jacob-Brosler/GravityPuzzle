using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : MonoBehaviour {
    public bool toggleState = true;

    public void ToggleState()
    {
        toggleState = !toggleState;
    }
}
