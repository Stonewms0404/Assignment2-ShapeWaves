using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource MainMenuMusic;
    [SerializeField] private AudioSource LevelLivesMusic;
    [SerializeField] private AudioSource LevelTimedMusic;

    private void OnEnable()
    {
        LevelLoader.PlayMusic += PlayMusic;
    }
    private void OnDisable()
    {
        LevelLoader.PlayMusic -= PlayMusic;
    }

    private void PlayMusic(LevelLoader.SceneType scene)
    {
        switch(scene)
        {
            case LevelLoader.SceneType.MAINMENU:
                MainMenuMusic.Play();
                LevelLivesMusic.Pause();
                LevelTimedMusic.Pause();
                break;
            case LevelLoader.SceneType.LIVES:
                MainMenuMusic.Pause();
                LevelLivesMusic.Play();
                LevelTimedMusic.Pause();
                break;
            case LevelLoader.SceneType.TIMED:
                MainMenuMusic.Pause();
                LevelLivesMusic.Pause();
                LevelTimedMusic.Play();
                break;
        }
    }
}
