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

    void Start()
    {

    }

    public void StartGame() => SceneManager.LoadScene(1);
 

    public void SetPlayerName()
    {
        if (MainManager.Instance != null)
        {
            SetPlayerText();
        }
    }

    private void SetPlayerText()
    {
        string text = playerNameInput.text;
        MainManager.Instance.currentPlayerName = text;
    }

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