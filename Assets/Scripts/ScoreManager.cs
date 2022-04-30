using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Might be useful to look into
// https://iqcode.com/code/csharp/unity-c-static-monobehaviour

// Possibly make this into a static class
public class ScoreManager : MonoBehaviour
{
    // Todo: Make the game not actually start before spacebar is pressed.

    // Persistant score for player
    public int totalScore = 0;

    // Volatile score used for pipe gap spacing.
    public int curScore = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text = totalScore.ToString();
        curScore = 0;
    }

    public void IncrementScore(int score)
    {
        totalScore += score;
        curScore += score;
        UpdateScore();
    }

    public void UpdateScore()
    {
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text = totalScore.ToString();
    }
}