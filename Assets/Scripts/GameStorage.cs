using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStorage : MonoBehaviour {
    public static GameObject goalPrefab;
    public static GameObject wallPrefab;
    public static GameObject teleporterPrefab;
    //rotated for left, right and down only walls. Default is up
    public static GameObject oneWayWallPrefab;
    public static GameObject spikePrefab;
    public static GameObject lockBoxPrefab;
    public static GameObject switchPrefab;
    public static GameObject playerPrefab;
    public static GameObject enemyPrefab;
    public static GameObject fakePlayerPrefab;
    public static Material playerMaterial;
    public static Material enemyMaterial;

    public static void LoadResources()
    {
        goalPrefab = Resources.Load("Prefabs/Goal") as GameObject;
        wallPrefab = Resources.Load("Prefabs/Wall") as GameObject;
        teleporterPrefab = Resources.Load("Prefabs/Teleporter") as GameObject;
        oneWayWallPrefab = Resources.Load("Prefabs/OneWayWall") as GameObject;
        spikePrefab = Resources.Load("Prefabs/Spikes") as GameObject;
        lockBoxPrefab = Resources.Load("Prefabs/Lock") as GameObject;
        switchPrefab = Resources.Load("Prefabs/Switch") as GameObject;
        playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
        fakePlayerPrefab = Resources.Load("Prefabs/PlayerStandin") as GameObject;
        playerMaterial = Resources.Load("Materials/Player") as Material;
        enemyMaterial = Resources.Load("Materials/Enemy") as Material;
    }
}
