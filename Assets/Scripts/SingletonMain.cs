using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMain<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null) SetupInstance();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null) SetupInstance();
        else  Destroy(gameObject);
    }

    private static void SetupInstance()
    {
        _instance = (T)FindObjectOfType(typeof(T));

        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            
            _instance = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }
}