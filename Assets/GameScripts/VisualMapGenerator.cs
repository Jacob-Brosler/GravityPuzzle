using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualMapGenerator : MonoBehaviour {

    GameObject[,] visualMap = new GameObject[20, 20];
    List<Map> mapList = new List<Map>();
    int currentMap = 0;
    public int moving = 0;
    Vector3 rotation = new Vector3(0, 0, 3);
    public List<Player> playerList = new List<Player>();
    public List<Player> enemyList = new List<Player>();
    //d = down = default, l = left, u = up, = right
    public char gravity = 'd';
    public bool falling = false;

    public LevelEditor customLevel;
    public bool playingCustom = false;

    public UIScript UIController;

    public Text flavorText;

	// Use this for initialization
	void Start () {
        GameStorage.LoadResources();
        MakeMaps();
	}

    public void StartGame(int l)
    {
        //currentMap = l;
        playingCustom = false;
        currentMap = 4;
        GenerateMap();
    }
	
	// Update is called once per frame
	void Update () {
		if(moving > 0)
        {
            transform.Rotate(rotation);
            moving--;
            if (moving == 0)
                FinishRotation();
        }
        else if(moving < 0)
        {
            transform.Rotate(-rotation);
            moving++;
            if (moving == 0)
                FinishRotation();
        }
	}

    public void FinishRotation()
    {
        foreach (Player player in playerList)
        {
            player.CanMove();
            player.transform.position = new Vector3(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
        }
        foreach (Player player in enemyList)
        {
            player.CanMove();
            player.transform.position = new Vector3(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
        }
        foreach (Vector2Int p in mapList[currentMap].TPLinkList.Values)
        {
            visualMap[p.x, p.y].GetComponent<Teleporter>().enabled = true;
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
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                Destroy(visualMap[x, y]);
                visualMap[x, y] = null;
            }
        }
        int ct = playerList.Count;
        for (int i = 0; i < ct; i++)
        {
            if (playerList[0] != null)
                Destroy(playerList[0].gameObject);
            playerList.RemoveAt(0);
        }
        ct = enemyList.Count;
        for (int i = 0; i < ct; i++)
        {
            if (enemyList[0] != null)
                Destroy(enemyList[0].gameObject);
            enemyList.RemoveAt(0);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        gravity = 'd';
    }

    public void GenerateMap()
    {
        Debug.Log("generating stuff");
        //clears the map
        ClearEverything();

        flavorText.text = mapList[currentMap].mapMessage;

        //draws the map
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if(mapList[currentMap].map[x,y] == 1)
                {
                    visualMap[x, y] = Instantiate(GameStorage.goalPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                }
                else if (mapList[currentMap].map[x, y] == 2)
                {
                    visualMap[x, y] = Instantiate(GameStorage.wallPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                }
                else if (mapList[currentMap].map[x, y] == 3)
                {
                    visualMap[x, y] = Instantiate(GameStorage.teleporterPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                }
                else if (mapList[currentMap].map[x, y] == 4)
                {
                    visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10 - 0.25f, 10 - x), Quaternion.Euler(0, 0, 90), transform);
                    visualMap[x, y].layer = 8;
                }
                else if (mapList[currentMap].map[x, y] == 5)
                {
                    visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10, 10 - x + 0.25f), Quaternion.Euler(0, 0, 0), transform);
                    visualMap[x, y].layer = 9;
                }
                else if (mapList[currentMap].map[x, y] == 6)
                {
                    visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10 + 0.25f, 10 - x), Quaternion.Euler(0, 0, -90), transform);
                    visualMap[x, y].layer = 10;
                }
                else if (mapList[currentMap].map[x, y] == 7)
                {
                    visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10, 10 - x - 0.25f), Quaternion.Euler(0, 0, 180), transform);
                    visualMap[x, y].layer = 11;
                }
                else if (mapList[currentMap].map[x, y] == 8)
                {
                    visualMap[x, y] = Instantiate(GameStorage.spikePrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(45, 45, 0), transform);
                }
                else if (mapList[currentMap].map[x, y] == 9)
                {
                    visualMap[x, y] = Instantiate(GameStorage.lockBoxPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                }
                else if (mapList[currentMap].map[x, y] == 10)
                {
                    visualMap[x, y] = Instantiate(GameStorage.switchPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
                }
                else if (mapList[currentMap].map[x, y] == 11)
                {
                    playerList.Add(Instantiate(GameStorage.playerPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform).GetComponent<Player>());
                    playerList[playerList.Count - 1].playerID = playerList.Count - 1;
                    Debug.Log("Spawning Player");
                }
                else if (mapList[currentMap].map[x, y] == 12)
                {
                    enemyList.Add(Instantiate(GameStorage.playerPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform).GetComponent<Player>());
                    enemyList[enemyList.Count - 1].playerID = enemyList.Count - 1;
                    enemyList[enemyList.Count - 1].GetComponent<Renderer>().material = GameStorage.enemyMaterial;
                    enemyList[enemyList.Count - 1].TurnToEnemy();
                }
                if (visualMap[x, y] != null)
                    visualMap[x, y].GetComponent<BlockInfo>().arrayPosition = new Vector2Int(x, y);
            }
        }
        falling = false;
    }

    public void RunCustomMap(Map m)
    {
        playingCustom = true;
        mapList.Add(m);
        UIController.ToGame();
        currentMap = mapList.Count - 1;
        GenerateMap();
    }

    public void CheckTriggerer(int x, int y, int playerID, bool enemy)
    {
        if (mapList[currentMap].map[x, y] == 1)
        {
            if (!enemy)
            {
                for (int p = playerID + 1; p < playerList.Count; p++)
                {
                    playerList[p].playerID--;
                }
                Destroy(playerList[playerID].gameObject);
                playerList.RemoveAt(playerID);
                if (playerList.Count == 0)
                {
                    if (playingCustom)
                    {
                        mapList.RemoveAt(mapList.Count - 1);
                        ClearEverything();
                        UIController.ToEditor();
                        customLevel.BackToEditing();
                    }
                    else
                    {
                        currentMap++;
                        if (currentMap >= mapList.Count)
                            currentMap = 0;
                        GenerateMap();
                    }
                }
            }
            else
            {
                GenerateMap();
            }
        }
        else if (mapList[currentMap].map[x, y] == 3)
        {
            if (visualMap[mapList[currentMap].TPLinkList[new Vector2Int(x, y)].x, mapList[currentMap].TPLinkList[new Vector2Int(x, y)].y].GetComponent<Teleporter>().enabled)
            {
                if(!enemy)
                    playerList[playerID].transform.position = visualMap[mapList[currentMap].TPLinkList[new Vector2Int(x, y)].x, mapList[currentMap].TPLinkList[new Vector2Int(x, y)].y].transform.position;
                else
                    enemyList[playerID].transform.position = visualMap[mapList[currentMap].TPLinkList[new Vector2Int(x, y)].x, mapList[currentMap].TPLinkList[new Vector2Int(x, y)].y].transform.position;
                visualMap[x, y].GetComponent<Teleporter>().enabled = false;
            }
        }
        else if (mapList[currentMap].map[x, y] == 8)
        {
            if (!enemy)
            {
                GenerateMap();
            }else
            {
                for (int p = playerID + 1; p < enemyList.Count; p++)
                {
                    enemyList[p].playerID--;
                }
                Destroy(enemyList[playerID].gameObject);
                enemyList.RemoveAt(playerID);
            }
        }
        else if (mapList[currentMap].map[x, y] == 10)
        {
            visualMap[mapList[currentMap].SwitchLinkList[new Vector2Int(x, y)].x, mapList[currentMap].SwitchLinkList[new Vector2Int(x, y)].y].GetComponent<SwitchBox>().ToggleState();
        }
    }

    //sets up the map to start rotating and changes the gravity identifier
    public void rotate(int r)
    {
        if (moving == 0 && !falling && AllPlayersStopped())
        {
            foreach (Player player in playerList)
            {
                player.Stop();
            }
            foreach (Player player in enemyList)
            {
                player.Stop();
            }
            moving = r;
            int layer = 0;
            if((gravity.Equals('u') && moving > 0) || (gravity.Equals('d') && moving < 0))
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
            foreach (Player player in playerList)
            {
                player.gameObject.layer = layer;
            }
            foreach (Player player in enemyList)
            {
                player.gameObject.layer = layer;
            }
        }
    }

    public bool AllPlayersStopped()
    {
        foreach(Player p in playerList)
        {
            if (p.MyRigidbody.velocity != Vector3.zero)
                return false;
        }
        foreach (Player p in enemyList)
        {
            if (p.MyRigidbody.velocity != Vector3.zero)
                return false;
        }
        return true;
    }
}
