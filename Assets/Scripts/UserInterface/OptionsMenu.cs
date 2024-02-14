using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    bool isActive;

    public GameObject optionsMenuUI;
    public GameObject mainMenuUI;

    MainMenu mainMenu;

    private void Start()
    {
        isActive = false;

        mainMenu = mainMenuUI.GetComponent<MainMenu>();

        optionsMenuUI.SetActive(false);
    }

    public void DisplayMenu()
    {
        isActive = !isActive;
        optionsMenuUI.SetActive(isActive);
        mainMenuUI.SetActive(!isActive);
    }

    public void LoadMenu()
    {
        DisplayMenu();
        mainMenu.ShowMenu();
    }
}
