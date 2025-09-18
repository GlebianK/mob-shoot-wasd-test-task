using System.Collections.Generic;
using UnityEngine;

public class GunMultiple : GunBase
{
    [SerializeField] private int projectilesPerShot = 3;

    private List<GameObject> projectiles;
    private List<Vector2> projectileShootDirections;

    protected override void Awake()
    {
        base.Awake();
        InitializeLists();
        SetProjectileDirections();
    }

    private void InitializeLists()
    {
        projectiles = new List<GameObject>(projectilesPerShot);
        projectileShootDirections = new List<Vector2>(projectilesPerShot);

        for (int i = 0; i < projectilesPerShot; i++)
        {
            projectiles.Add(null);
            projectileShootDirections.Add(Vector2.zero);
        }
    }

    private void ResetProjectilesList()
    {
        if (projectiles == null || projectiles.Count < projectilesPerShot)
        {
            Debug.LogWarning($"GunMultiple -> ResetProjectilesList -> Smth is wrong with list! List count: {projectiles.Count}");
            return;
        }

        for (int i = 0; i < projectiles.Count; i++)
            projectiles[i] = null;
    }

    private void SetProjectileDirections()
    {
        if (projectilesPerShot < 1)
            throw new System.ArgumentException($"No projectiles for gun: {gameObject.name}");

        if (projectilesPerShot == 1)
        {
            projectileShootDirections[0] = new Vector2(1f, 0f);       
        }
        else if (projectilesPerShot % 2 != 0)
        {
            for (int i = 0; i < projectilesPerShot; i++)
            {
                //Debug.Log($"i: {i}/{projectilesPerShot - 1}");
                float x = (float)(projectilesPerShot - i)/ (float)projectilesPerShot;

                float y = i/ (float)projectilesPerShot;
                if (i > 0 && i % 2 == 0)
                    y *= -1;

                Vector2 temp = new Vector2(x, y);
                projectileShootDirections[i] = temp;
            }
        }
        else
        {
            for (int i = 0; i < projectilesPerShot; i++)
            {
                float x = (float)(projectilesPerShot - i) / (float)projectilesPerShot;

                float y = ((float)i + 0.5f) / (float)projectilesPerShot;
                if (i > 0 && i % 2 == 0)
                    y *= -1;

                Vector2 temp = new Vector2(x, y);
                projectileShootDirections[i] = temp;
            }
        }
    }

    protected override GameObject TakeFromPool()
    {
        GameObject temp = projectilePool.Dequeue();
        //Debug.Log($"GunMultiple: Took projectile from pool! Pool size: {projectilePool.Count}");
        return temp;
    }

    public override void Shoot()
    {
        if (!canShoot)
            return;

        if (projectilePool.Count < projectilesPerShot)
            return;

        //Debug.LogWarning("ÁÀÌ!");
        for (int i = 0; i < projectiles.Count; i++)
        {
            GameObject temp = TakeFromPool();
            if (temp == null)
                return;
            projectiles[i] = temp;
        }
        
        for (int i = 0; i < projectiles.Count; i++)
        {
            GameObject projectileUnit = projectiles[i];
            projectileUnit.SetActive(true);
            if (projectileUnit.TryGetComponent<Projectile>(out Projectile projectileComponent))
            {
                projectileComponent.SetShootDirection(projectileShootDirections[i]);
                projectileComponent.FireProjectile();
            }
            else
                Debug.LogError("Gun -> Shoot -> No projectile component for current projectile!");
        }

        StartCoroutine(ShootCooldown());
        ResetProjectilesList();
    }
}
