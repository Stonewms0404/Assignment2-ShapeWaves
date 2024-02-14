using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    bool isActive;

    public GameObject creditsMenuUI;
    public GameObject mainMenuUI;

    MainMenu mainMenu;

    private void Start()
    {
        isActive = false;
        creditsMenuUI.SetActive(false);

        mainMenu = mainMenuUI.GetComponent<MainMenu>();
    }

    public void DisplayMenu()
    {
        isActive = !isActive;
        creditsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        DisplayMenu();
        mainMenu.ShowMenu();
    }
}
