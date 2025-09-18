using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Player/Add Projectile")]
public class ProjectileType : ScriptableObject
{
    public float projectileDamage;
    public float projectileSpeed;
}
