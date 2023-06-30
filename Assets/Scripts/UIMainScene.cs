using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIMainScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameoverObject;
    [SerializeField] Rigidbody Ball;

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
            SetPrefab();
        }
    }

    void SetPrefab() =>
        MainManager.Instance.SetPrefabConfig();

    void SetPlayerText(string text) =>
        playerName.text = text;

    string HasBestScoreName =>
        MainManager.Instance.DataRawName();

    string HasBestScore =>
        MainManager.Instance.DataRawScore().ToString();

    void SetScoreText(TextMeshProUGUI scoreText) =>
        MainManager.Instance.ScoreText = scoreText;

    void SetBallRigidbody() =>
        MainManager.Instance.Ball = Ball;

    void SetGameoverText() =>
        MainManager.Instance.GameOverText = gameoverObject;

    public void GoToMenu() =>
        SceneManager.LoadScene(0);

}