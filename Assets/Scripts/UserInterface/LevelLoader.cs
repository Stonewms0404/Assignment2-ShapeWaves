using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static event Action<SceneType> PlayMusic;

    public Animator transition;
    public float transitionTime;

    public DeathMenu.GameType gameType;

    public enum SceneType { MAINMENU, LIVES, TIMED };
    public SceneType scene;

    private void Start()
    {
        SetSceneType(scene);
    }

    public void SetSceneType(SceneType value)
    {
        scene = value;
        PlayMusic(scene);
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
