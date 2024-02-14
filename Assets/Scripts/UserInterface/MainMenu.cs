using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject playMenuObj;
    public GameObject creditsMenuObj;
    public LevelLoader levelLoader;

    bool isActive;

    PlayMenu playMenu;
    CreditsMenu creditsMenu;

    private void Start()
    {
        isActive = true;
        mainMenuUI.SetActive(isActive);

        levelLoader.SetSceneType(LevelLoader.SceneType.MAINMENU);

        playMenu = playMenuObj.GetComponent<PlayMenu>();
        creditsMenu = creditsMenuObj.GetComponent<CreditsMenu>();
    }

    public void ShowMenu()
    {
        isActive = true;
        mainMenuUI.SetActive(isActive);
    }

    public void Play()
    {
        isActive = false;
        mainMenuUI.SetActive(isActive);

        playMenuObj.SetActive(!isActive);
    }

    public void Credits()
    {
        isActive = false;
        mainMenuUI.SetActive(isActive);

        creditsMenuObj.SetActive(!isActive);
    }
}
