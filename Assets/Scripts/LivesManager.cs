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
        FindObjectOfType<DifficultyManager>().ShowMenu();
        Debug.Log("Game Over");
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
            return;
        }

        Invoke("ResetScene", 2f);
    }

    public void ResetScene()
    {
        Debug.Log("Reset Scene");
        resetEvent?.Invoke();
    }
}