using UnityEngine;

public class GunSingle : GunBase
{
    protected override GameObject TakeFromPool()
    {
        GameObject temp = projectilePool.Dequeue();
        Debug.Log($"GunSingle: Took projectile from pool! Pool size: {projectilePool.Count}");
        return temp;
    }
}
