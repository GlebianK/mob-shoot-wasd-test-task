using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{
    [SerializeField] private GameObject prevWeaponButton;
    [SerializeField] private GameObject nextWeaponButton;
    [SerializeField] private TMP_Text currentWeapon;
    [SerializeField] private bool isTopPanel;

    private PlayerShoot shooting;
    private string[] weapons = { "Gun", "Shotgun", "Rifle" }; // Заглушка

    private void Start()
    {
        GameObject player = FindAnyObjectByType<PlayerShoot>().gameObject;
        if (player.TryGetComponent<PlayerShoot>(out shooting))
        {
            shooting.WeaponChanged.AddListener(OnWeaponChanged);
        }
        else
        {
            throw new System.ArgumentNullException("WeaponsPanel -> Couldn't find player shoot component!");
        }

        if (GameManager.Instance.CurrentDeviceType == DeviceType.Desktop)
        {
            prevWeaponButton.SetActive(false);
            nextWeaponButton.SetActive(false);
        }
        else
        {
            if (isTopPanel)
                gameObject.SetActive(false);
        }

        currentWeapon.text = weapons[shooting.CurrentGunID];
    }

    private void OnDisable()
    {
        if (shooting != null)
        {
            shooting.WeaponChanged.RemoveListener(OnWeaponChanged);
        }
    }

    public void OnPressPrevWeapon()
    {
        if (shooting != null)
            shooting.SwitchGun(-1);
    }

    public void OnPressNextWeapon()
    {
        if (shooting != null)
            shooting.SwitchGun(1);
    }

    public void OnWeaponChanged()
    {
        currentWeapon.text = weapons[shooting.CurrentGunID];
    }
}
