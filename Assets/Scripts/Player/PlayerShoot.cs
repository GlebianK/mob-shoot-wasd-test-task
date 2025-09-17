using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject gunPivot;
    [SerializeField] private GameObject[] gunPrefabs;
    [SerializeField] private float gunSwitchCooldown = 0.75f;

    private List<GameObject> guns;
    private List<Gun> gunComponents;
    private Gun currentGun;
    private int currentGunId;

    private bool canSwitchGun;

    private void Awake()
    {
        guns = new();
        gunComponents = new();

        if (gunPrefabs == null || gunPrefabs.Length < 1)
        {
            canSwitchGun = false;
            throw new System.ArgumentNullException("PlayerShoot: No guns for player!");
        }

        currentGunId = 0;

        foreach (GameObject gun in gunPrefabs) // Создаём объекты-оружия из префабов
        {
            GameObject temp_gun = Instantiate(gun, gunPivot.transform.position, Quaternion.identity, gunPivot.transform);
            temp_gun.transform.localPosition = new Vector3(temp_gun.transform.localPosition.x + temp_gun.transform.localScale.x / 2,
                temp_gun.transform.localPosition.y, temp_gun.transform.localPosition.z);
            guns.Add(temp_gun);

            if (temp_gun.TryGetComponent<Gun>(out Gun gunComponent))
                gunComponents.Add(gunComponent);
            else
                Debug.LogError("Player shoot -> Awake -> Couldn't find Gun component!");
        }

        for (int i = 0; i < guns.Count; i++) // Отсключаем "лишние" оружия
        {
            if (i != currentGunId)
                guns[i].SetActive(false);
            else 
                guns[i].SetActive(true);
        }
        
        foreach (Gun gun in gunComponents) // Инициализируем компоненты Gun (из ScriptableObject'ов) и соответствующие прожектайлы
        {
            gun.InitializeGun();
            gun.InitializeProjectiles();
        }

        currentGun = gunComponents[currentGunId];

        canSwitchGun = true;
    }

    #region CUSTOM PRIVATE METHODS
    private void ShootGun()
    {
        currentGun.Shoot();
    }

    private void SwitchGun(int prevOrNext)
    {
        Debug.Log($"Switching gun... Input parameter is {prevOrNext}");

        if (!canSwitchGun)
            return;

        if (prevOrNext > 1)
            prevOrNext = 1;
        else if (prevOrNext < -1)
            prevOrNext = -1;

        currentGunId += prevOrNext;

        if (currentGunId >= guns.Count)
            currentGunId = 0;
        else if (currentGunId < 0)
            currentGunId = guns.Count - 1;

        Debug.Log($"Cur gun: {currentGun.gameObject.name}");
        currentGun.gameObject.SetActive(false);

        currentGun = gunComponents[currentGunId];

        currentGun.gameObject.SetActive(true);
        Debug.Log($"Cur gun: {currentGun.gameObject.name}");
        Debug.Log($"PlayerShoot: Gun switched! Current gun id: {currentGunId}");
        StartCoroutine(SwitchGunCD());
    }
    #endregion

    #region INPUT SYSTEM CALLBACKS
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootGun();
        }
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log($"Scroll detected! Value: {context.ReadValue<Vector2>()}, action: {context.action}");
            Vector2 temp = context.ReadValue<Vector2>();
            SwitchGun((int)temp.y);
        }
    }
    #endregion

    private IEnumerator SwitchGunCD()
    {
        canSwitchGun = false;
        yield return new WaitForSeconds(gunSwitchCooldown);
        canSwitchGun = true;
        yield return null;
    }
    
}
