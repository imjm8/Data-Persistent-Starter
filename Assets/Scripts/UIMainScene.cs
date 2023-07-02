using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIMainScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private GameObject gameoverObject;
    [SerializeField] private Rigidbody Ball;

    void Awake()
    {
        SetPlayerName();
        SetScoreTextMesh();
        SetBallRigidbody();
        SetGameoverText();
        DrawPrefab();
    }

    private void DrawPrefab() =>
        MainManager.Instance.DrawPrefabConfig();

    private void SetPlayerName() =>
        playerName.text = MainManager.Instance.currentPlayerName;

    private void SetScoreTextMesh() =>
        MainManager.Instance.bestScoreMesh = scoreTextMesh;

    private void SetBallRigidbody() =>
        MainManager.Instance.Ball = Ball;

    private void SetGameoverText() =>
        MainManager.Instance.GameEndedTextObject = gameoverObject;

    public void GoToMenu() =>
        SceneManager.LoadScene(0);

}