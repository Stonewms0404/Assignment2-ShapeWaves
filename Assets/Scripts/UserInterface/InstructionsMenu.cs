using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    bool isActive;

    public GameObject instructionsMenuUI;
    public GameObject playMenuUI;

    PlayMenu playMenu;

    private void Start()
    {
        isActive = false;
        instructionsMenuUI.SetActive(isActive);

        playMenu = playMenuUI.GetComponent<PlayMenu>();
    }

    public void DisplayMenu()
    {
        isActive = !isActive;
        instructionsMenuUI.SetActive(false);
        playMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        DisplayMenu();
        playMenu.DisplayMenu();
    }
}
