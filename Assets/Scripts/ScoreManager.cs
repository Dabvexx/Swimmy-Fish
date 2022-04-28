using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int totalScore;
    public void IncrementScore(int score)
    {
        totalScore += score;
    }
}
