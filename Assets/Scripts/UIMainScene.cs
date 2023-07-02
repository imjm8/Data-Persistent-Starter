using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIMainScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI topScorerTitle;
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private GameObject gameoverObject;
    [SerializeField] private Rigidbody Ball;

    void Awake()
    {
        SetTopScorer();
        SetIngameScoreTextMesh();
        SetBallRigidbody();
        SetGameoverText();
        DrawPrefab();
    }

    private void DrawPrefab() =>
        MainManager.Instance.DrawPrefabConfig();

    private void SetTopScorer() =>
        MainManager.Instance.LoadBestScoreText(topScorerTitle);

    private void SetIngameScoreTextMesh() =>
        MainManager.Instance.inGameScoreMesh = scoreTextMesh;

    private void SetBallRigidbody() =>
        MainManager.Instance.Ball = Ball;

    private void SetGameoverText() =>
        MainManager.Instance.gameEndedTextObject = gameoverObject;

    public void GoToMenu() =>
        SceneManager.LoadScene(0);

}