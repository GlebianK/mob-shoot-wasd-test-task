using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text hpText;

    private float hpAmount;
    private float playerMaxHP;
    private Health playerHealth;

    private void Start()
    {
        GameObject player = FindAnyObjectByType<PlayerControl>().gameObject;
        if (player.TryGetComponent<Health>(out playerHealth))
        {
            playerHealth.TookDamage.AddListener(OnHPChanged);
        }
        else
        {
            throw new System.ArgumentNullException("HpBar -> Couldn't find player health!");
        }

        hpAmount = playerHealth.HP;
        playerMaxHP = hpAmount;
        hpBar.fillAmount = 1;
        hpText.text = hpAmount.ToString();
    }

    private void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.TookDamage.RemoveListener(OnHPChanged);
    }

    public void OnHPChanged()
    {
        hpAmount = playerHealth.HP;

        if (hpAmount < 0)
            hpAmount = 0;

        hpText.text = hpAmount.ToString();
        hpBar.fillAmount = (float)hpAmount / (float)playerMaxHP;
    }
}
