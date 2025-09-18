using UnityEngine;

public class GunSingle : GunBase
{
    protected override GameObject TakeFromPool()
    {
        if (projectilePool.Count < 1)
            return null;

        GameObject temp = projectilePool.Dequeue();
        Debug.Log($"GunSingle: Took projectile from pool! Pool size: {projectilePool.Count}");
        return temp;
    }
}
