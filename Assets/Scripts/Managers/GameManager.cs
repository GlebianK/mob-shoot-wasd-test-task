using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

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

        Application.targetFrameRate = 60;

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

        /*
        Debug.LogWarning("Device Type: " + SystemInfo.deviceType);
        Debug.LogWarning("Device Model: " + SystemInfo.deviceModel);
        Debug.LogWarning("Device Name: " + SystemInfo.deviceName);
        Debug.LogWarning("Operating System: " + SystemInfo.operatingSystem);
        Debug.LogWarning("Graphics Device Type: " + SystemInfo.graphicsDeviceType);
        Debug.LogWarning("System Memory Size: " + SystemInfo.systemMemorySize + " MB");
        */
    }

}
