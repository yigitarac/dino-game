using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private enum GameState { Start, Playing, GameOver }
    private const string HighScoreKey = "DinoHighScore";
    private const float ScorePerSecond = 10f;
    private GameState state = GameState.Start;
    private float score;
    private float highScore;
    private GUIStyle scoreStyle;
    private GUIStyle messageStyle;
    public bool IsPlaying => state == GameState.Playing;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance == null)
        {
            new GameObject("GameManager").AddComponent<GameManager>();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetFloat(HighScoreKey, 0f);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (state == GameState.Playing)
        {
            score += Time.deltaTime * ScorePerSecond;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == GameState.Start)
            {
                StartGame();
            }
            else if (state == GameState.GameOver)
            {
                RestartGame();
            }
        }
    }

    public void EndGame()
    {
        if (state != GameState.Playing) return;

        state = GameState.GameOver;
        Time.timeScale = 0f;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    private void StartGame()
    {
        state = GameState.Playing;
        score = 0f;
        Time.timeScale = 1f;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        state = GameState.Start;
    }

    private void OnGUI()
    {
        InitStyles();

        const float width = 500f;
        const float padding = 20f;
        float rowHeight = scoreStyle.CalcHeight(new GUIContent("Score"), width);

        GUI.Label(new Rect(Screen.width - width - padding, padding, width, rowHeight), $"Score: {Mathf.FloorToInt(score)}", scoreStyle);
        GUI.Label(new Rect(Screen.width - width - padding, padding + rowHeight, width, rowHeight), $"High Score: {Mathf.FloorToInt(highScore)}", scoreStyle);

        if (state == GameState.Start)
        {
            DrawCenteredMessage("Press SPACE to play");
        }
        else if (state == GameState.GameOver)
        {
            DrawCenteredMessage("You Died\nPress SPACE to try again");
        }
    }

    private void DrawCenteredMessage(string message)
    {
        const float width = 500f;
        const float height = 100f;
        Rect rect = new Rect((Screen.width - width) / 2f, (Screen.height - height) / 2f, width, height);
        GUI.Label(rect, message, messageStyle);
    }

    private void InitStyles()
    {
        if (scoreStyle == null)
        {
            scoreStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 44,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperRight,
                wordWrap = false
            };
            scoreStyle.normal.textColor = Color.white;
        }

        if (messageStyle == null)
        {
            messageStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 36,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };
            messageStyle.normal.textColor = Color.white;
        }
    }
}
