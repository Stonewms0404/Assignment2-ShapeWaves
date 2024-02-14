using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    bool isActive;

    public GameObject playMenuUI;
    public GameObject mainMenuUI;
    public GameObject instructionsMenuUI;
    public LevelLoader levelLoader;

    MainMenu mainMenu;
    InstructionsMenu instructionsMenu;

    private void Start()
    {
        isActive = false;
        mainMenu = mainMenuUI.GetComponent<MainMenu>();
        instructionsMenu = instructionsMenuUI.GetComponent<InstructionsMenu>();

        playMenuUI.SetActive(false);
    }

    public void DisplayMenu()
    {
        isActive = !isActive;
        playMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void DisplayInstructions()
    {
        isActive = !isActive;
        playMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        DisplayMenu();
        mainMenu.ShowMenu();
    }

    public void LoadLevel(int value)
    {
        levelLoader.GoToScene(value);
    }
}
