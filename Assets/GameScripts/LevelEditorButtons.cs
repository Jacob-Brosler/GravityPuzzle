using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorButtons : MonoBehaviour {
    public Vector2Int position;

    public LevelEditor levelEditor;

    public void OnClick()
    {
        levelEditor.ChangeTile(position);
    }
}
