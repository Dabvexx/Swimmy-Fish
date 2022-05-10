using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    #region Variables

    private ScoreManager sm;
    private LivesManager lm;
    private PipeGenerator pg;

    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private TextMeshProUGUI highScoreText;

    #endregion Variables

    // Start is called before the first frame update
    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        lm = FindObjectOfType<LivesManager>();
        pg = FindObjectOfType<PipeGenerator>();
        gameCanvas.SetActive(false);
    }

    public void EasyMode()
    {
        pg.pipeGap = 3f;
        pg.pipeDelay = 4.5f;
        pg.pipeSpeed = 2.5f;
        pg.variationIntensity = 4.74f;
        StartGame();
    }

    public void MediumMode()
    {
        pg.pipeGap = 2.7f;
        pg.pipeDelay = 2.5f;
        pg.pipeSpeed = 5f;
        pg.variationIntensity = 8.3f;
        StartGame();
    }

    public void HardMode()
    {
        pg.pipeGap = 2.35f;
        pg.pipeDelay = .5f;
        pg.pipeSpeed = 7f;
        pg.variationIntensity = 10.2f;
        StartGame();
    }

    private void StartGame()
    {
        // Turn on all scripts to start gameplay.
        gameCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        lm.ResetScene();
    }

    public void ShowMenu()
    {
        // Reset values.
        lm.lives = 3;
        sm.totalScore = 0;
        GameObject.FindGameObjectWithTag("Lives").GetComponent<TextMeshProUGUI>().text = $"Lives: 3";
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text = "0";

        // Render menu.
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        highScoreText.text = "High Score: " + sm.highScore.ToString();
    }
}