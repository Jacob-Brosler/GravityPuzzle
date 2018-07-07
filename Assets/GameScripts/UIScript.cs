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

    public Text currentSkinText;
    public int currentSkin = 0;
    public List<string> skinNameList = new List<string> { "Dungeon", "Fantasy", "Cute" };

    public Slider MusicVolumeSlider;
    public Text MusicVolumeText;
    public int MusicVolume;
    public Slider SFXVolumeSlider;
    public Text SFXVolumeText;
    public int SFXVolume;

    void Start()
    {
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

        MainMenu.SetActive(true);
        LevelSelect.SetActive(false);
        OptionsMenu.SetActive(false);
        GameUI.SetActive(false);
        EditUI.SetActive(false);
    }

    public void ToLevelSelect()
    {
        MainMenu.SetActive(false);
        EditUI.SetActive(false);
        LevelSelect.SetActive(true);
    }

    public void ToMain()
    {
        MainMenu.SetActive(true);
        LevelSelect.SetActive(false);
        OptionsMenu.SetActive(false);
        GameUI.SetActive(false);
    }

    public void ToOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void ToGame()
    {
        LevelSelect.SetActive(false);
        GameUI.SetActive(true);
        EditUI.SetActive(false);
    }

    public void ToEditor()
    {
        LevelSelect.SetActive(false);
        GameUI.SetActive(false);
        EditUI.SetActive(true);
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
