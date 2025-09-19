using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject tutorialCanvas;

    private DeviceType deviceType;

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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
