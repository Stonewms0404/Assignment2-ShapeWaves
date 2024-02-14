using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action<int> _TimeOut;

    float timer;
    float totalTimeSurvived;
    bool isPlayerDead;
    bool timedOut = false;

    [SerializeField] private GameObject timerTextObj;
    [SerializeField] private DeathMenu deathMenu;
    private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        Player._PickupItem += AddTime;
        Player._HitObject += PauseTimer;
        Player._NotDead += PauseTimer;
        Waves._NextWaveMultiplier += AddWaveTime;
    }
    private void OnDisable()
    {
        Player._PickupItem -= AddTime;
        Player._HitObject -= PauseTimer;
        Player._NotDead -= PauseTimer;
        Waves._NextWaveMultiplier += AddWaveTime;
    }

    private void PauseTimer(bool playerDead)
    {
        isPlayerDead = playerDead;
    }

    private void Start()
    {
        timer = 100;
        timerText = timerTextObj.GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (!isPlayerDead && !timedOut)
        {
            if (timer <= 0)
            {
                timedOut = true;
                _TimeOut((int)totalTimeSurvived);
            }
            else
            {
                timer -= Time.deltaTime;
                totalTimeSurvived += Time.deltaTime;
            }
        }
        DisplayTimer();
    }

    private void AddWaveTime(int amount)
    {
        AddTime(amount + 5);
    }

    public void AddTime(int amount)
    {
        timer += amount;
    }

    public void DisplayTimer()
    {
        timerText.text = "Timer: " + (int)timer;
    }
}
