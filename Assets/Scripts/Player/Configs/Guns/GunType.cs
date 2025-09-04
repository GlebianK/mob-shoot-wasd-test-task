using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Player/Add Gun")]
public class GunType : ScriptableObject
{
    public string gunName;
    public ShootType gunShootType;
    public float gunCooldownBetweenShots;
    public GameObject gunProjectilePrefab;

    public enum ShootType
    {
        single,
        multiple,
        burst
    }
}
