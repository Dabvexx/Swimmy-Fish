using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Todo: Make the game not actually start before spacebar is pressed.

    private int totalScore;
    private int lives = 3;

    [SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private TextMeshProUGUI livesText;

    private void Start()
    {
        FindObjectOfType<CollisionDetection>().deathEvent += DecrementLives;
    }

    public void IncrementScore(int score)
    {
        totalScore += score;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = totalScore.ToString();
    }

    public void OnGameOver()
    {
        // Prompt player to restart or go to title screen.
        // Title screen will have many fishies spinning and flying across the screen.
    }

    private void DecrementLives()
    {
        lives--;

        // I For the life of me cant figure out why this will give a null refrence exception but ok sure.
        //livesText.text = "Lives: " + lives;

        // Commedically long single line to make the god damn thing work for some god forsaken reason.
        GameObject.FindGameObjectWithTag("Lives").GetComponent<TextMeshProUGUI>().text = $"Lives: {lives}";

        if (lives == 0)
        {
            // Invoke to get timing
            Invoke("OnGameOver", 2f);
        }

        Invoke("ResetScene", 2f);
    }

    private void ResetScene()
    {
        // Figure out scene persistance.
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}