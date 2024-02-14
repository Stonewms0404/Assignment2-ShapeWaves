using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    public static event Action<int> _GameOver;
    public static event Action<int> _AddToScore;

    int Lives;

    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;

    private void OnEnable()
    {
        Player._HitObject += LostLife;
        Player._AddLife += AddLife;
    }
    private void OnDisable()
    {
        Player._HitObject -= LostLife;
        Player._AddLife -= AddLife;
    }

    private void Start()
    {
        Lives = 3;

        life3.SetActive(true);
        life2.SetActive(true);
        life1.SetActive(true);
    }


    private void AddLife(int value)
    {
        if (Lives == 3)
        {
            _AddToScore(UnityEngine.Random.Range(5, 11));
        }
        else
        {
            ++Lives;
            ChangeLivesDisplay();
        }
    }
    private void LostLife(bool playerDead)
    {
        if (playerDead)
        {
            --Lives;
            ChangeLivesDisplay();
        }
    }

    private void ChangeLivesDisplay()
    {
        Debug.Log("Lives " + Lives);
        if (Lives == 3)
        {
            life3.SetActive(true);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        if (Lives == 2)
        {
            life3.SetActive(false);
            life2.SetActive(true);
            life1.SetActive(true);
        }
        else if (Lives == 1)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(true);
        }
        else if (Lives == 0)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);
            _GameOver(0);
        }
    }
}
