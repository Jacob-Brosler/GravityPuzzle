using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject GameUI;
    public GameObject LevelSelect;
    public GameObject OptionsMenu;
    public GameObject EditUI;
    public GameObject LevelSelectButton;
    public GameObject LevelSelectScroll;
    public GameObject LevelLock;
    public GameObject PauseMenu;

    public Text currentSkinText;
    public int currentSkin = 0;
    public List<string> skinNameList = new List<string> { "Dungeon", "Fantasy", "Cute" };

    public Slider MusicVolumeSlider;
    public Text MusicVolumeText;
    public int MusicVolume;
    public Slider SFXVolumeSlider;
    public Text SFXVolumeText;
    public int SFXVolume;

    private Button OptionsBack;

    void Start()
    {
        OptionsBack = GameObject.Find("OptionsBack").GetComponent<Button>();


        if (!PlayerPrefs.HasKey("CurrentSkin"))
        {
            PlayerPrefs.SetInt("CurrentSkin", 0);
        }
        currentSkin = PlayerPrefs.GetInt("CurrentSkin");

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetInt("MusicVolume", 100);
        }
        MusicVolume = PlayerPrefs.GetInt("MusicVolume");

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetInt("SFXVolume", 100);
        }
        SFXVolume = PlayerPrefs.GetInt("SFXVolume");

        MusicVolumeSlider.value = MusicVolume;
        SFXVolumeSlider.value = SFXVolume;
        updateSkinText();
        UpdateMusicVolume();
        UpdateSFXVolume();

        ToMain();
        EditUI.SetActive(false);
        PauseMenu.SetActive(false);

        Debug.Log("generate buttons");
        int xPos = 60;
        int yPos = -20;
        VisualMapGenerator mapCont = GameObject.Find("MapController").GetComponent<VisualMapGenerator>();
        for(int i = 0; i < mapCont.MapCount; i++)
        {
            GameObject button = Instantiate(LevelSelectButton, LevelSelectScroll.transform);
            button.transform.localPosition = new Vector3(xPos, yPos, 0);
            int temp = i;
            button.GetComponent<Button>().onClick.AddListener(delegate { mapCont.StartGame(temp); });
            button.GetComponent<Button>().onClick.AddListener(delegate { ToGame(); });
            button.GetComponentInChildren<Text>().text = "Level " + (i + 1);
            xPos += 90;
            if(xPos > 420)
            {
                xPos = 60;
                yPos -= 50;
            }
            if(PlayerPrefs.GetInt("UnlockedLevel" + i, 0) == 0)
            {
                GameObject locked = Instantiate(LevelLock, button.transform);
                locked.name = "LockLevel" + i;
            }
        }
    }

    public void ToLevelSelect()
    {
        MainMenu.SetActive(false);
        EditUI.SetActive(false);
        LevelSelect.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void ToMain()
    {
        Debug.Log("ToMain");
        MainMenu.SetActive(true);
        LevelSelect.SetActive(false);
        OptionsMenu.SetActive(false);
        GameUI.SetActive(false);
        OptionsBack.onClick.RemoveAllListeners();
        OptionsBack.onClick.AddListener(delegate { ToMain(); });
    }

    public void ToOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void ToGame()
    {
        LevelSelect.SetActive(false);
        GameUI.SetActive(true);
        EditUI.SetActive(false);
        PauseMenu.SetActive(false);
    }

    public void ToEditor()
    {
        LevelSelect.SetActive(false);
        GameUI.SetActive(false);
        PauseMenu.SetActive(false);
        EditUI.SetActive(true);
    }

    public void ToPause()
    {
        OptionsMenu.SetActive(false);
        if (GameObject.Find("MapController").GetComponent<VisualMapGenerator>().AllFallersStopped())
        {
            PauseMenu.SetActive(true);
            OptionsBack.onClick.RemoveAllListeners();
            OptionsBack.onClick.AddListener(delegate { ToPause(); });
            if (GameObject.Find("MapController").GetComponent<VisualMapGenerator>().playingCustom)
            {
                GameObject.Find("PauseMenuBackText").GetComponent<Text>().text = "Back to Editor";
                GameObject.Find("PauseMenuBack").GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("MapController").GetComponent<VisualMapGenerator>().FinishCustomTest(); });
                GameObject.Find("PauseMenuBack").GetComponent<Button>().onClick.AddListener(delegate { ToEditor(); });
            }
            else
            {
                GameObject.Find("PauseMenuBackText").GetComponent<Text>().text = "Back to Level Select";
                GameObject.Find("PauseMenuBack").GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("MapController").GetComponent<VisualMapGenerator>().ClearEverything(); });
                GameObject.Find("PauseMenuBack").GetComponent<Button>().onClick.AddListener(delegate { ToLevelSelect(); });
            }
        }
    }

    public void previousSkin()
    {
        currentSkin--;
        if (currentSkin == -1)
            currentSkin = skinNameList.Count - 1;
        updateSkinText();
    }

    public void nextSkin()
    {
        currentSkin++;
        if (currentSkin == skinNameList.Count)
            currentSkin = 0;
        updateSkinText();
    }

    public void updateSkinText()
    {
        currentSkinText.text = skinNameList[currentSkin];
    }

    public void UpdateMusicVolume()
    {
        MusicVolumeText.text = "Music Volume: " + MusicVolumeSlider.value;
        MusicVolume = (int)MusicVolumeSlider.value;
    }

    public void UpdateSFXVolume()
    {
        SFXVolumeText.text = "SFX Volume: " + SFXVolumeSlider.value;
        SFXVolume = (int)SFXVolumeSlider.value;
    }

    public void SaveChanges()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkin);
        PlayerPrefs.SetInt("MusicVolume", MusicVolume);
        PlayerPrefs.SetInt("SFXVolume", SFXVolume);
    }
}
