using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject parentCanvas;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void OnPressStart()
    {
        Time.timeScale = 1.0f;
        parentCanvas.SetActive(false);
    }
}
