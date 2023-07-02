using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] TextMeshProUGUI bestScore;

    void Start()
    {
        LoadBestScore();
    }

    public void StartGame() => 
        SceneManager.LoadScene(1);

    private void LoadBestScore() => 
        MainManager.Instance.LoadBestScoreText(bestScore);

    public void SetPlayerName() => 
        MainManager.Instance.currentPlayerName = playerNameInput.text;

    public void Exit()
    {
        MainManager.Instance.SaveBestScore();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}