using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    private int Score = 0;
    private int Multiplier;
    
    public void AddScore(int amount)
    {
        Score += amount * Multiplier;
    }

    public int GetScore()
    {
        return Score;
    }

    public void SetMultiplier(int amount)
    {
        Multiplier = amount;
    }

    public int GetMultiplier()
    {
        return Multiplier;
    }
}
