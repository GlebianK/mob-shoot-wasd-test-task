using UnityEngine;

public class InputPanelController : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.CurrentDeviceType == DeviceType.Desktop)
            gameObject.SetActive(false);
    }
}
