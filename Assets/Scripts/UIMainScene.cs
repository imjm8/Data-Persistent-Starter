using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIMainScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameoverObject;
    [SerializeField] private Rigidbody Ball;

    void Awake()
    {
        if (MainManager.Instance != null)
        {
            // SetPlayerText(HasBestScoreName != null
            //                 ? HasBestScoreName + " has best score: " + HasBestScore
            //                 : MainManager.Instance.currentPlayerName);
            SetPlayerText(MainManager.Instance.currentPlayerName);
            SetScoreText(scoreText);
            SetBallRigidbody();
            SetGameoverText();
            DrawPrefab();
        }
    }

    private void DrawPrefab() =>
        MainManager.Instance.DrawPrefabConfig();

    private void SetPlayerText(string text) =>
        playerName.text = text;

    private string HasBestScoreName =>
        MainManager.Instance.DataNameRaw();

    private string HasBestScore =>
        MainManager.Instance.DataScoreRaw().ToString();

    private void SetScoreText(TextMeshProUGUI scoreText) =>
        MainManager.Instance.ScoreText = scoreText;

    private void SetBallRigidbody() =>
        MainManager.Instance.Ball = Ball;

    private void SetGameoverText() =>
        MainManager.Instance.GameOverText = gameoverObject;

    private void GoToMenu() =>
        SceneManager.LoadScene(0);

}