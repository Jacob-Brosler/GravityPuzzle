using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    //0 = nothing, 1 = goal, 2 = wall, 3 = teleporter, 4 = left only wall, 5 = up only wall, 6 = right only wall, 7 = down only wall, 8 = spike, 9 = locked box, 10 = switch, 11 = player start
    public int[,] map;
    public Dictionary<Vector2Int, Vector2Int> TPLinkList;
    public Dictionary<Vector2Int, Vector2Int> SwitchLinkList;

    public Map(int[,] map)
    {
        this.map = map;
        TPLinkList = new Dictionary<Vector2Int, Vector2Int>();
        SwitchLinkList = new Dictionary<Vector2Int, Vector2Int>();
    }

    public void AddTP(Vector2Int p1, Vector2Int p2)
    {
        TPLinkList.Add(p1, p2);
        TPLinkList.Add(p2, p1);
    }

    public void AddSwitch(Vector2Int switchyBoi, Vector2Int box)
    {
        SwitchLinkList.Add(switchyBoi, box);
    }
}
