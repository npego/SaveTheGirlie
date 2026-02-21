using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject winUI;

    [Space]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;

    private int killCount;
    private float runTimer;
    private bool stopTimer;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;

        runTimer = 0f;
        stopTimer = false;

        killCount = 0;
        killCountText.text = "0";
    }

    private void Update()
    {
        if (!stopTimer)
            runTimer += Time.unscaledDeltaTime; // funciona aunque hagas Time.timeScale = 0.5

        timerText.text = runTimer.ToString("F2") + "s";
    }

    public void EnableGameOverUI()
    {
        stopTimer = true;
        Time.timeScale = .5f;
        gameOverUI.SetActive(true);
        var spawner = FindFirstObjectByType<Enemy_Respawner>();
        if (spawner != null)
            spawner.StopSpawning();
    }

    public void EnableGameWinUI()
    {
        stopTimer = true;
        Time.timeScale = .5f;
        winUI.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();

        if (killCount >= 10)
        {
            EnableGameWinUI();
        }
    }
}