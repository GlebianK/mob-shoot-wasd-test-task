using TMPro;
using UnityEngine;

public class VictoryCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        int timer = (int)GameManager.Instance.GameTimer;
        Debug.LogWarning($"Timer: {timer}");
    }
}
