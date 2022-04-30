using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    public static LivesManager instance;

    public delegate void ResetDelegate();

    public event ResetDelegate resetEvent;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        FindObjectOfType<CollisionDetection>().deathEvent += DecrementLives;
        GameObject.FindGameObjectWithTag("Lives").GetComponent<TextMeshProUGUI>().text = $"Lives: {lives}";
    }

    public void OnGameOver()
    {
        // Prompt player to restart or go to title screen.
        // Title screen will have many fishies spinning and flying across the screen.
    }

    private void DecrementLives()
    {
        FindObjectOfType<CollisionDetection>().deathEvent -= DecrementLives;
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
        resetEvent?.Invoke();
    }
}