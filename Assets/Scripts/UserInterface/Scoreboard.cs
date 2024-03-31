using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public static event Action<int> _GameOver;
    public static event Action<int, int> _GameOverTimer;

    [SerializeField] private GameObject scorePanelObj;
    [SerializeField] private GameObject scoreTextObj;
    [SerializeField] private GameObject multiplerTextObj;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI multiplierText;

    private int score;
    private int multipler = 1;

    private void OnEnable()
    {
        Timer._TimeOut += PostFinalScoreTimer;
        LivesDisplay._GameOver += PostFinalScore;
        Enemy._AddToScore += SetScore;
        LivesDisplay._AddToScore += SetScore;
        Player._HitObject += ResetMultipler;
        Waves._NextWaveMultiplier += SetMultiplier;
    }
    private void OnDisable()
    {
        Timer._TimeOut -= PostFinalScoreTimer;
        LivesDisplay._GameOver -= PostFinalScore;
        Enemy._AddToScore -= SetScore;
        LivesDisplay._AddToScore += SetScore;
        Player._HitObject -= ResetMultipler;
        Waves._NextWaveMultiplier -= SetMultiplier;
    }

    private void Start()
    {
        scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
        multiplierText = multiplerTextObj.GetComponent<TextMeshProUGUI>();

        SetScore(0);
    }

    private void SetScore(int amount)
    {
        score += amount * multipler;
        SetScoreText();
    }

    private void SetMultiplier(int wave)
    {
        multipler *= 2;
        SetMultiplierText();
    }

    private void ResetMultipler(bool isDead)
    {
        multipler = 1;
        SetMultiplierText();
    }

    private void SetScoreText()
    {
        scoreText.SetText(score + " Points");
    }

    private void SetMultiplierText()
    {
        multiplierText.text = ("Multipler: " + multipler + "x");
    }

    private void PostFinalScore(int value)
    {
        _GameOver(score);
    }
    private void PostFinalScoreTimer(int timeSurvived)
    {
        _GameOverTimer(score, timeSurvived);
    }
}