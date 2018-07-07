using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    Map customMap = new Map();
    public VisualMapGenerator mappyBoi;
    GameObject[,] visualMap = new GameObject[20, 20];
    public GameObject tileChangeButton;
    public GameObject editUIParent;
    GameObject[,] editButtons = new GameObject[20, 20];
    public GameObject testButton;
    public Text infoText;

    public bool makingTP = false;
    public Vector2Int tpSpot1;

    public bool makingSwitch = false;
    public Vector2Int lockedBoxPos;

    public int tileToPlace = 2;

    void Start()
    {
        int buttonX = -176;
        int buttonY;
        for (int x = 0; x < 20; x++)
        {
            buttonY = 158;
            for (int y = 0; y < 20; y++)
            {
                Debug.Log(x + " " + y + "|" + buttonX + " " + buttonY);
                editButtons[x, y] = Instantiate(tileChangeButton, editUIParent.transform);
                editButtons[x, y].transform.localPosition = new Vector3(buttonX, buttonY);
                editButtons[x, y].GetComponent<LevelEditorButtons>().position = new Vector2Int(y, x);
                editButtons[x, y].GetComponent<LevelEditorButtons>().levelEditor = this;
                if (y == 0 || y == 18 || y % 2 == 1)
                {
                    buttonY -= 18;
                }
                else
                {
                    buttonY -= 17;
                }
            }
            if (x == 0 || x == 18 || x % 2 == 1)
            {
                buttonX += 18;
            }
            else
            {
                buttonX += 17;
            }
        }
    }

    public void LoadLevel(int l)
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                //grabs the value
                customMap.map[x, y] = PlayerPrefs.GetInt("CustomLevel" + l + "" + x + "" + y, 0);
                ChangeVisual(x, y);
            }
        }
        //checks if map is playable
        if (CheckFor(1) && CheckFor(11))
        {
            testButton.SetActive(true);
        }
        else
        {
            testButton.SetActive(false);
        }

        //adds all the saved TP values
        int p = 1;
        string s = PlayerPrefs.GetString("Level" + l + "TP" + p, "");
        while (!s.Equals(""))
        {
            int firstSpace = s.IndexOf(' ');
            int secondSpace = s.IndexOf(' ', firstSpace + 1);
            int thirdSpace = s.IndexOf(' ', secondSpace + 1);
            customMap.AddTP(new Vector2Int(int.Parse(s.Substring(0, firstSpace)), int.Parse(s.Substring(firstSpace + 1, secondSpace - (firstSpace + 1)))), new Vector2Int(int.Parse(s.Substring(secondSpace + 1, thirdSpace - (secondSpace + 1))), int.Parse(s.Substring(thirdSpace + 1))));
            p++;
            s = PlayerPrefs.GetString("Level" + l + "TP" + p, "");
        }

        //adds all the saved switch values
        p = 1;
        s = PlayerPrefs.GetString("Level" + l + "Switch" + p, "");
        while (!s.Equals(""))
        {
            int firstSpace = s.IndexOf(' ');
            int secondSpace = s.IndexOf(' ', firstSpace + 1);
            int thirdSpace = s.IndexOf(' ', secondSpace + 1);
            customMap.AddSwitch(new Vector2Int(int.Parse(s.Substring(0, firstSpace)), int.Parse(s.Substring(firstSpace + 1, secondSpace - (firstSpace + 1)))), new Vector2Int(int.Parse(s.Substring(secondSpace + 1, thirdSpace - (secondSpace + 1))), int.Parse(s.Substring(thirdSpace + 1))));
            p++;
            s = PlayerPrefs.GetString("Level" + l + "Switch" + p, "");
        }
        tileToPlace = 2;
        infoText.text = "Selected Block: Wall";
        makingTP = false;
        makingSwitch = false;
    }

    public void SaveLevel(int l)
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                PlayerPrefs.SetInt("CustomLevel" + l + "" + x + "" + y, customMap.map[x, y]);
            }
        }

        //adds all the saved TP values
        int p = 1;
        string s = PlayerPrefs.GetString("Level" + l + "TP" + p, "");
        while (s.CompareTo("") != 0)
        {
            PlayerPrefs.DeleteKey("Level" + l + "TP" + p);
            p++;
            s = PlayerPrefs.GetString("Level" + l + "TP" + p, "");
        }
        p = 1;
        foreach (Vector2Int v in customMap.TPLinkList.Keys)
        {
            PlayerPrefs.SetString("Level" + l + "TP" + p, v.x + " " + v.y + " " + customMap.TPLinkList[v].x + " " + customMap.TPLinkList[v].y);
            p++;
        }

        //adds all the saved switch values
        p = 1;
        s = PlayerPrefs.GetString("Level" + l + "Switch" + p, "");
        while (s.CompareTo("") != 0)
        {
            PlayerPrefs.DeleteKey("Level" + l + "Switch" + p);
            p++;
            s = PlayerPrefs.GetString("Level" + l + "Switch" + p, "");
        }
        p = 1;
        foreach (Vector2Int v in customMap.SwitchLinkList.Keys)
        {
            PlayerPrefs.SetString("Level" + l + "TP" + p, v.x + " " + v.y + " " + customMap.SwitchLinkList[v].x + " " + customMap.SwitchLinkList[v].y);
            p++;
        }
    }

    public void ChangeTileToPlay(int tileType)
    {
        makingSwitch = false;
        makingTP = false;
        tileToPlace = tileType;
        if(tileType == 0)
        {
            infoText.text = "Remove Blocks";
        }
        else if (tileType == 1)
        {
            infoText.text = "Selected Block: Goal";
        }
        else if (tileType == 2)
        {
            infoText.text = "Selected Block: Wall";
        }
        else if (tileType == 4)
        {
            infoText.text = "Selected Block: Left Wall";
        }
        else if (tileType == 5)
        {
            infoText.text = "Selected Block: Up Wall";
        }
        else if (tileType == 6)
        {
            infoText.text = "Selected Block: Right Wall";
        }
        else if (tileType == 7)
        {
            infoText.text = "Selected Block: Down Wall";
        }
        else if (tileType == 8)
        {
            infoText.text = "Selected Block: Spike";
        }
        else if (tileType == 11)
        {
            infoText.text = "Selected Block: Player";
        }
        else if (tileType == 11)
        {
            infoText.text = "Selected Block: Enemy";
        }
    }

    public void ChangeTile(Vector2Int tile)
    {
        if (makingTP)
        {
            if (tpSpot1 == new Vector2Int(-1, -1))
            {
                tpSpot1 = tile;
                infoText.text = "Now choose where to place the second one.";
            }
            else
            {
                CheckToRemove(tile);
                customMap.map[tpSpot1.x, tpSpot1.y] = 3;
                customMap.map[tile.x, tile.y] = 3;
                customMap.AddTP(tpSpot1, tile);
                visualMap[tpSpot1.x, tpSpot1.y] = Instantiate(GameStorage.teleporterPrefab, new Vector3(tpSpot1.y - 10, 10 - tpSpot1.x), Quaternion.Euler(0, 0, 0), transform);
                visualMap[tile.x, tile.y] = Instantiate(GameStorage.teleporterPrefab, new Vector3(tile.y - 10, 10 - tile.x), Quaternion.Euler(0, 0, 0), transform);
                tpSpot1 = new Vector2Int(-1, -1);
                infoText.text = "Choose where to place the first teleporter.";
            }
        }
        else if (makingSwitch)
        {
            if (lockedBoxPos == new Vector2Int(-1, -1))
            {
                lockedBoxPos = tile;
                infoText.text = "Now choose where to place the switch.";
            }
            else
            {
                CheckToRemove(lockedBoxPos);
                CheckToRemove(tile);
                customMap.map[lockedBoxPos.x, lockedBoxPos.y] = 9;
                customMap.map[tile.x, tile.y] = 10;
                customMap.AddSwitch(tile, lockedBoxPos);
                visualMap[lockedBoxPos.x, lockedBoxPos.y] = Instantiate(GameStorage.lockBoxPrefab, new Vector3(lockedBoxPos.y - 10, 10 - lockedBoxPos.x), Quaternion.Euler(0, 0, 0), transform);
                visualMap[tile.x, tile.y] = Instantiate(GameStorage.switchPrefab, new Vector3(tile.y - 10, 10 - tile.x), Quaternion.Euler(0, 0, 0), transform);
                lockedBoxPos = new Vector2Int(-1, -1);
                infoText.text = "Choose where to place the locked box.";
            }
        }
        else
        {
            CheckToRemove(tile);
            customMap.map[tile.x, tile.y] = tileToPlace;
            Debug.Log(customMap.map[tile.x, tile.y] + "=" + tileToPlace);
            ChangeVisual(tile.x, tile.y);
            //makes sure there is at least one player and at least one goal on the map
            if (CheckFor(1) && CheckFor(11))
            {
                testButton.SetActive(true);
            }
            else
            {
                testButton.SetActive(false);
            }
        }
    }

    public void ChangeVisual(int x, int y)
    {
        Debug.Log("Changing Visual: " + customMap.map[x, y]);
        if (visualMap[x, y] != null)
            Destroy(visualMap[x, y]);
        if (customMap.map[x, y] == 1)
        {
            visualMap[x, y] = Instantiate(GameStorage.goalPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 2)
        {
            visualMap[x, y] = Instantiate(GameStorage.wallPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 3)
        {
            visualMap[x, y] = Instantiate(GameStorage.teleporterPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 4)
        {
            visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10 - 0.25f, 10 - x), Quaternion.Euler(0, 0, 90), transform);
        }
        else if (customMap.map[x, y] == 5)
        {
            visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10, 10 - x + 0.25f), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 6)
        {
            visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10 + 0.25f, 10 - x), Quaternion.Euler(0, 0, -90), transform);
        }
        else if (customMap.map[x, y] == 7)
        {
            visualMap[x, y] = Instantiate(GameStorage.oneWayWallPrefab, new Vector3(y - 10, 10 - x - 0.25f), Quaternion.Euler(0, 0, 180), transform);
        }
        else if (customMap.map[x, y] == 8)
        {
            visualMap[x, y] = Instantiate(GameStorage.spikePrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(45, 45, 0), transform);
        }
        else if (customMap.map[x, y] == 9)
        {
            visualMap[x, y] = Instantiate(GameStorage.lockBoxPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 10)
        {
            visualMap[x, y] = Instantiate(GameStorage.switchPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 11)
        {
            visualMap[x, y] = Instantiate(GameStorage.fakePlayerPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
        }
        else if (customMap.map[x, y] == 12)
        {
            visualMap[x, y] = Instantiate(GameStorage.fakePlayerPrefab, new Vector3(y - 10, 10 - x), Quaternion.Euler(0, 0, 0), transform);
            visualMap[x, y].GetComponent<Renderer>().material = GameStorage.enemyMaterial;
        }
    }

    public void CheckToRemove(Vector2Int p)
    {
        if (customMap.TPLinkList.ContainsKey(p))
        {
            Debug.Log("There's a tp here");
            customMap.map[p.x, p.y] = 0;
            customMap.map[customMap.TPLinkList[p].x, customMap.TPLinkList[p].y] = 0;
            ChangeVisual(customMap.TPLinkList[p].x, customMap.TPLinkList[p].y);
            customMap.TPLinkList.Remove(customMap.TPLinkList[p]);
            customMap.TPLinkList.Remove(p);
        }
        if (customMap.SwitchLinkList.ContainsKey(p))
        {
            customMap.map[p.x, p.y] = 0;
            customMap.map[customMap.SwitchLinkList[p].x, customMap.SwitchLinkList[p].y] = 0;
            ChangeVisual(customMap.SwitchLinkList[p].x, customMap.SwitchLinkList[p].y);
            customMap.SwitchLinkList.Remove(p);
        }
        if (customMap.SwitchLinkList.ContainsValue(p))
        {
            foreach (Vector2Int key in customMap.SwitchLinkList.Keys)
            {
                if (customMap.SwitchLinkList[key] == p)
                {
                    customMap.map[key.x, key.y] = 0;
                    customMap.map[p.x, p.y] = 0;
                    ChangeVisual(key.x, key.y);
                    customMap.SwitchLinkList.Remove(key);
                }
            }
        }
        ChangeVisual(p.x, p.y);
    }

    public void TestLevel()
    {

        BreakDownLevel();
        mappyBoi.RunCustomMap(customMap);
    }

    public void BreakDownLevel()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (visualMap[x, y] != null)
                    Destroy(visualMap[x, y]);
            }
        }
    }

    public void BackToEditing()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                ChangeVisual(x, y);
            }
        }
    }

    public void MakeTP()
    {
        makingSwitch = false;
        makingTP = true;
        tpSpot1 = new Vector2Int(-1, -1);
        infoText.text = "Choose where to place the first teleporter.";
    }

    public void MakeSwitch()
    {
        makingTP = false;
        makingSwitch = true;
        lockedBoxPos = new Vector2Int(-1, -1);
        infoText.text = "Choose where to place the locked box.";
    }

    public bool CheckFor(int b)
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                if (customMap.map[x, y] == b)
                    return true;
            }
        }
        return false;
    }
}
