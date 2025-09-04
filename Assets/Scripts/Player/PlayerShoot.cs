using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject gunPivot;
    [SerializeField] private Gun[] guns;
    [SerializeField] private float gunSwitchCooldown = 0.75f;

    private Gun currentGun;
    private int currentGunId;

    private bool canSwitchGun;

    private void Awake()
    {
        if (guns == null)
        {
            throw new System.ArgumentNullException("PlayerShoot: No guns for player!");
        }

        currentGun = guns[0];
        currentGunId = 0;
        foreach (Gun gun in guns)
        {
            gun.InitializeGun();
        }

        canSwitchGun = true;

        Gun temp = Instantiate(currentGun, gunPivot.transform.position, Quaternion.identity, gunPivot.transform);
        temp.transform.localPosition = new Vector3(temp.transform.localPosition.x + temp.transform.localScale.x,
            temp.transform.localPosition.y, temp.transform.localPosition.z);
    }

    private void ShootGun()
    {
        currentGun.Shoot();
    }

    private void SwitchGun(int prevOrNext) // TODO: добавить вызов этого метода в соответствующий коллбэк ниже
    {
        if (!canSwitchGun)
            return;

        if (prevOrNext != -1 || prevOrNext != 1)
        {
            Debug.LogError("PlayerShoot: Wrong gun switch!");
            prevOrNext = 0;
        }

        currentGunId += prevOrNext;

        if (currentGunId >= guns.Length)
            currentGunId = guns.Length - 1;
        else if (currentGunId < 0)
            currentGunId = 0;

        currentGun = guns[currentGunId];
        Debug.Log($"PlayerShoot: Gun switched! Current gun id: {currentGunId}");
        StartCoroutine(SwitchGunCD());
    }

    // TODO: добавить событие-смену оружия (колёсико или кнопки) и его коллбэк

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootGun();
        }
    }

    private IEnumerator SwitchGunCD()
    {
        canSwitchGun = false;
        yield return new WaitForSeconds(gunSwitchCooldown);
        canSwitchGun = true;
        yield return null;
    }
    
}
