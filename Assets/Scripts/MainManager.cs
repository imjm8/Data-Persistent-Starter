using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainManager : MonoBehaviour
{
    private static MainManager _Instance;
    public static MainManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                SetupInstance();
            }

            return _Instance;
        }
    }

    [SerializeField] private Brick _BrickPrefab;
    [SerializeField] private float _force = 2.0f;
    private int LineCount = 6;
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameEnded = false;
    private bool m_IsWin = false;
    public bool isWin { get => m_IsWin; }

    private Rigidbody _Ball;
    public Rigidbody Ball
    {
        private get => _Ball;
        set => _Ball = value;
    }

    public TextMeshProUGUI topScoreMesh { get; set; }
    public TextMeshProUGUI inGameScoreMesh { get; set; }
    public string currentPlayerName { get; set; }
    public GameObject gameEndedTextObject { get; set; }

    private static void SetupInstance()
    {
        _Instance = FindObjectOfType<MainManager>();
        if (_Instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = "MainManager";

            _Instance = gameObj.AddComponent<MainManager>();
            DontDestroyOnLoad(gameObj);
        }
    }

    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // CurrentPlayerName = PlayerDataHandle.Instance.PlayerName;
    }

    public void DrawPrefabConfig() => _SetPrefabConfig();
    private void _SetPrefabConfig()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(_BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        m_Started = false;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.5f, 1.5f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                _Ball.transform.SetParent(null);
                _Ball.AddForce(forceDir * _force, ForceMode.VelocityChange);
            }
        }
        else if (m_GameEnded)
        {
            if (isWin) LoadBestScoreText(topScoreMesh);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                m_Started = false;
            }
        }
    }

    private void AddPoint(int point)
    {
        m_Points += point;
        inGameScoreMesh.text = $"Score : {m_Points}";
    }

    private void Win()
    {
        TextMeshProUGUI textMesh = gameEndedTextObject.GetComponent<TextMeshProUGUI>();
        textMesh.text = "Congratulations! \nPress Space to play again...";
        gameEndedTextObject.SetActive(true);
        LoadBestScoreText(topScoreMesh);
        m_GameEnded = true;
        m_IsWin = true;
        m_Points = 0;
    }

    private void GameOver()
    {
        m_GameEnded = true;
        m_Points = 0;
        LoadBestScoreText(topScoreMesh);
        gameEndedTextObject.SetActive(true);
    }

    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }

    public void SaveBestScore()
    {
        SaveData data = LoadData();

        if (m_Points > data.HighiestScore)
        {

            data.HighiestScore = m_Points;
            data.TheBestPlayer = currentPlayerName;

            Win();

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            Debug.Log(Application.persistentDataPath);

            LoadBestScoreText(topScoreMesh);
        }
        else if (m_Points <= data.HighiestScore)
        {
            GameOver();
            return;
        }
    }

    public void LoadBestScoreText(TextMeshProUGUI mesh)
    {
        topScoreMesh = mesh;
        SaveData data = LoadData();
        topScoreMesh.text = $"Best Score: {data.TheBestPlayer} : {data.HighiestScore}";
    }

    private SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data;
        }

        return new SaveData();
    }
}
