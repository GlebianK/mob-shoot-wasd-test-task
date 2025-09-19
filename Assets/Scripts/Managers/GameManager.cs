using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject victoryCanvas;

    private DeviceType deviceType;
    private int totalEnemiesToKill;
    private int kills;
    private float timer;
    private bool isCounting;

    public float GameTimer => timer;

    public DeviceType CurrentDeviceType 
    { 
        get 
        { 
            return deviceType; 
        } 
    }

    private void Awake()
    {
        Instance = this;

        Time.timeScale = 1.0f;

        totalEnemiesToKill = 0;
        kills = 0;

        Application.targetFrameRate = 60; // TODO: убрать в билде!

        switch (SystemInfo.deviceType)
        {
            case DeviceType.Desktop:
                deviceType = DeviceType.Desktop; 
                break;
            case DeviceType.Handheld:
                deviceType = DeviceType.Handheld;
                break;
            default:
                throw new System.Exception("Game Manager: Wrong device type!!!");
        }

        Debug.LogWarning($"Device type: {deviceType}");

        gameplayCanvas.SetActive(true);
        tutorialCanvas.SetActive(true);
        victoryCanvas.SetActive(false);

        /*
         * Шпаргалка
        Debug.LogWarning("Device Type: " + SystemInfo.deviceType);
        Debug.LogWarning("Device Model: " + SystemInfo.deviceModel);
        Debug.LogWarning("Device Name: " + SystemInfo.deviceName);
        Debug.LogWarning("Operating System: " + SystemInfo.operatingSystem);
        Debug.LogWarning("Graphics Device Type: " + SystemInfo.graphicsDeviceType);
        Debug.LogWarning("System Memory Size: " + SystemInfo.systemMemorySize + " MB");
        */
    }

    private void Start()
    {
        StopAllCoroutines();
        timer = 0f;
        isCounting = true;
        StartCoroutine(TimerCoroutine());
    }

    private void Victory()
    {
        victoryCanvas.SetActive(true);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddEnemyAmount(int numToAdd)
    {
        totalEnemiesToKill += numToAdd;
    }

    public void CountKills(int numToAdd)
    {
        kills += numToAdd;

        if (kills == totalEnemiesToKill)
        {
            Victory();
            Time.timeScale = 0.0f;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (isCounting)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
