using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualMapGenerator : MonoBehaviour
{

    List<Map> mapList = new List<Map>();
    int currentMap = 0;
    public int moving = 0;
    Vector3 rotation = new Vector3(0, 0, 3);
    public int playerCount;
    //d = down = default, l = left, u = up, = right
    public char gravity = 'd';

    public LevelEditor customLevel;
    public bool playingCustom = false;

    public UIScript UIController;

    public Text flavorText;

    public int MapCount
    {
        get
        {
            return mapList.Count;
        }
    }

    // Use this for initialization
    void Awake()
    {
        GameStorage.LoadResources();
        MakeMaps();
        PlayerPrefs.SetInt("UnlockedLevel0", 1);
    }

    public void StartGame(int l)
    {
        currentMap = l;
        playingCustom = false;
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving > 0)
        {
            transform.Rotate(rotation);
            moving--;
            if (moving == 0)
                FinishRotation();
        }
        else if (moving < 0)
        {
            transform.Rotate(-rotation);
            moving++;
            if (moving == 0)
                FinishRotation();
        }
    }

    public void FinishRotation()
    {
        foreach (Transform tempBlock in GetComponentsInChildren<Transform>())
        {
            if (tempBlock.GetComponent<Fallable>() != null)
            {
                tempBlock.GetComponent<Fallable>().Unfreeze();
                tempBlock.transform.position = new Vector3(Mathf.RoundToInt(tempBlock.transform.position.x), Mathf.RoundToInt(tempBlock.transform.position.y));
            }
            if (tempBlock.GetComponent<BlockInfo>() != null && tempBlock.GetComponent<BlockInfo>().blockType == Blocks.Teleporter)
                tempBlock.GetComponent<Toggleable>().toggleState = true;
        }
    }

    public void MakeMaps()
    {
        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "Press the buttons. Go on, they won't hurt you."));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 2, 11, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "The pill is your goal. No, that is not a drug joke."));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 2, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
                                                { 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "Same as before but the construction crew got lazy."));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 2, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "I hired an abstract artist for this one, do you like it?"));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 1, 0, 2, 2, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 2, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 6, 11, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "One way walls are only solid on, say it with me, one wall!"));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 2, 3, 0, 0, 0, 0, 0, 1, 2, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 11, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "Now we're thinking with portals."));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(5, 4), new Vector2Int(10, 9));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                                { 0, 0, 2, 1, 0, 0, 3, 2, 0, 0, 2, 0, 4, 3, 4, 0, 2, 0, 0, 0 },
                                                { 0, 0, 2, 2, 2, 2, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 11, 2, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "Put it all together and what do you get? A fun level!"));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(7, 6), new Vector2Int(7, 13));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 9, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 9, 0, 0, 0, 10, 2, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 10, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 11, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "Oh no, it's a block with no hands' worst nightmare, doors!"));
        mapList[mapList.Count - 1].AddSwitch(new Vector2Int(11, 8), new Vector2Int(10, 9));
        mapList[mapList.Count - 1].AddSwitch(new Vector2Int(10, 13), new Vector2Int(9, 8));

        mapList.Add(new Map(new int[20, 20] {   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 0, 3, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0 },
                                                { 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 8, 3, 0, 0, 0, 1, 2, 0, 0 },
                                                { 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0 },
                                                { 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 0, 0, 11, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                                                "Touch the grey spiky thing and you die. Got it? Good."));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(7, 6), new Vector2Int(9, 12));

        mapList.Add(new Map(new int[20, 20] {   { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 3, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 11, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 } },
                                                "Now that is a lot of portals."));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(18, 4), new Vector2Int(14, 4));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(14, 7), new Vector2Int(10, 7));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(10, 10), new Vector2Int(6, 10));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(6, 13), new Vector2Int(3, 13));
        mapList[mapList.Count - 1].AddTP(new Vector2Int(3, 16), new Vector2Int(1, 16));

    }

    public void ClearEverything()
    {
        foreach (Transform block in GetComponentsInChildren<Transform>(true))
        {
            if (block.GetComponent<BlockInfo>() != null)
                Destroy(block.gameObject);
        }

        playerCount = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gravity = 'd';
    }

    public void GenerateMap()
    {
        //clears the map
        ClearEverything();

        flavorText.text = mapList[currentMap].mapMessage;

        GameObject tempBlock = null;
        //draws the map
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                switch ((Blocks)mapList[currentMap].map[x, y])
                {
                    case Blocks.Goal:
                        tempBlock = Instantiate(GameStorage.goalPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        break;
                    case Blocks.Wall:
                        tempBlock = Instantiate(GameStorage.wallPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        break;
                    case Blocks.Teleporter:
                        tempBlock = Instantiate(GameStorage.teleporterPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        break;
                    case Blocks.LeftOnlyWall:
                        tempBlock = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10 - 0.25f, 10 - x), Quaternion.Euler(0, 0, 90), transform);
                        tempBlock.layer = 8;
                        tempBlock.GetComponent<BlockInfo>().blockType = Blocks.LeftOnlyWall;
                        break;
                    case Blocks.UpOnlyWall:
                        tempBlock = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10, 10 - x + 0.25f), Quaternion.Euler(0, 0, 0), transform);
                        tempBlock.layer = 9;
                        tempBlock.GetComponent<BlockInfo>().blockType = Blocks.UpOnlyWall;
                        break;
                    case Blocks.RightOnlyWall:
                        tempBlock = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10 + 0.25f, 10 - x), Quaternion.Euler(0, 0, -90), transform);
                        tempBlock.layer = 10;
                        tempBlock.GetComponent<BlockInfo>().blockType = Blocks.RightOnlyWall;
                        break;
                    case Blocks.DownOnlyWall:
                        tempBlock = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10, 10 - x - 0.25f), Quaternion.Euler(0, 0, 180), transform);
                        tempBlock.layer = 11;
                        tempBlock.GetComponent<BlockInfo>().blockType = Blocks.DownOnlyWall;
                        break;
                    case Blocks.Spike:
                        tempBlock = Instantiate(GameStorage.spikePrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(45, 45, 0), transform);
                        break;
                    case Blocks.LockBox:
                        tempBlock = Instantiate(GameStorage.lockBoxPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        break;
                    case Blocks.Switch:
                        tempBlock = Instantiate(GameStorage.switchPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        break;
                    case Blocks.PlayerStart:
                        tempBlock = Instantiate(GameStorage.playerPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        playerCount++;
                        Debug.Log("Spawning Player");
                        break;
                    case Blocks.EnemyStart:
                        tempBlock = Instantiate(GameStorage.enemyPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                        break;
                }
                if (tempBlock != null)
                {
                    tempBlock.GetComponent<BlockInfo>().arrayPosition = new Vector2Int(x, y);
                }
                tempBlock = null;
            }
        }

        foreach (LinkedBlock toLink in GetComponentsInChildren<LinkedBlock>())
        {
            //links all of the teleporters together
            if (mapList[currentMap].TPLinkList.ContainsKey(toLink.GetComponent<BlockInfo>().arrayPosition))
            {
                Debug.Log("Linking tps: " + toLink.GetComponent<BlockInfo>().arrayPosition + "|" + mapList[currentMap].TPLinkList[toLink.GetComponent<BlockInfo>().arrayPosition]);
                foreach (LinkedBlock block in GetComponentsInChildren<LinkedBlock>())
                {
                    if (block.GetComponent<BlockInfo>().arrayPosition == mapList[currentMap].TPLinkList[toLink.GetComponent<BlockInfo>().arrayPosition])
                    {
                        toLink.linkedBlock = block.gameObject;
                        block.linkedBlock = toLink.gameObject;
                        Debug.Log("Finished linking tps");
                    }
                }
            }

            //links all of the switch and locked boxes together
            if (mapList[currentMap].SwitchLinkList.ContainsKey(toLink.GetComponent<BlockInfo>().arrayPosition))
            {
                foreach (BlockInfo block in GetComponentsInChildren<BlockInfo>())
                {
                    if (block.arrayPosition == mapList[currentMap].SwitchLinkList[toLink.GetComponent<BlockInfo>().arrayPosition])
                        toLink.linkedBlock = block.gameObject;
                }
            }
        }
    }

    public void RunCustomMap(Map m)
    {
        playingCustom = true;
        mapList.Add(m);
        UIController.ToGame();
        currentMap = mapList.Count - 1;
        GenerateMap();
    }

    //sets up the map to start rotating and changes the gravity identifier
    public void rotate(int r)
    {
        if (moving == 0 && AllFallersStopped())
        {

            foreach (Fallable faller in GetComponentsInChildren<Fallable>())
            {
                faller.Freeze();
            }
            moving = r;
            int layer = 0;
            if ((gravity.Equals('u') && moving > 0) || (gravity.Equals('d') && moving < 0))
            {
                gravity = 'r';
                layer = 15;
            }
            else if ((gravity.Equals('d') && moving > 0) || (gravity.Equals('u') && moving < 0))
            {
                gravity = 'l';
                layer = 13;
            }
            else if ((gravity.Equals('l') && moving > 0) || (gravity.Equals('r') && moving < 0))
            {
                gravity = 'u';
                layer = 14;
            }
            else if ((gravity.Equals('r') && moving > 0) || (gravity.Equals('l') && moving < 0))
            {
                gravity = 'd';
                layer = 16;
            }
            foreach (Fallable faller in GetComponentsInChildren<Fallable>())
            {
                faller.gameObject.layer = layer;
            }
        }
    }

    public bool AllFallersStopped()
    {
        foreach (Fallable faller in GetComponentsInChildren<Fallable>())
        {
            if (!faller.Stopped())
                return false;
        }
        return true;
    }

    public void CheckForLevelCompletion()
    {
        if (playerCount == 0)
        {
            if (playingCustom)
            {
                FinishCustomTest();
            }
            else
            {
                currentMap++;
                if (currentMap >= mapList.Count)
                    currentMap = 0;
                if (PlayerPrefs.GetInt("UnlockedLevel" + currentMap, 0) == 0)
                {
                    PlayerPrefs.SetInt("UnlockedLevel" + currentMap, 1);
                    Destroy(GameObject.Find("LockLevel" + currentMap));
                }
                GenerateMap();
            }
        }
    }

    public void FinishCustomTest()
    {
        mapList.RemoveAt(mapList.Count - 1);
        ClearEverything();
        UIController.ToEditor();
        customLevel.BackToEditing();
    }
}
