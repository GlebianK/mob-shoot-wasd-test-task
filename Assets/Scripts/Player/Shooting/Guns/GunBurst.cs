using UnityEngine;

public class GunBurst : GunBase
{
    protected override GameObject TakeFromPool()
    {
        if (projectilePool.Count < 1)
            return null;

        GameObject temp = projectilePool.Dequeue();
        //Debug.Log($"GunBurst: Took projectile from pool! Pool size: {projectilePool.Count}");
        return temp;
    }

    public override void Shoot()
    {
        return;
    }

    private void Update()
    {
        if (IsTriggerPulled)
            base.Shoot();
    }
}
