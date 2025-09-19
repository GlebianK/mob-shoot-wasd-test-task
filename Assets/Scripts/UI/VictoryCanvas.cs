using TMPro;
using UnityEngine;

public class VictoryCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text hpText;

    private void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        float timer = GameManager.Instance.GameTimer;
        timerText.text = "Your time: " + timer.ToString("F1") + " seconds";

        GameObject player = FindAnyObjectByType<PlayerControl>().gameObject;
        if (player.TryGetComponent<Health>(out Health playerHealth))
        {
            float temp = playerHealth.HP;
            hpText.text = "Your HP: " + temp.ToString("0");
        }
        else
        {
            hpText.text = "Your HP: Not Found";
        }
    }
}
