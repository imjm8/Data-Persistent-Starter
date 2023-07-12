using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
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
            else 
            {
                string typeName = typeof(T).Name;
            }

            return _instance;
        }
    }

    private void Awake()
        {
            // if this is the first instance, make this the persistent singleton
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                DontDestroyOnLoad(this.gameObject);
            }
            // otherwise, remove any duplicates
            else
            {
                Destroy(gameObject);
            }
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
