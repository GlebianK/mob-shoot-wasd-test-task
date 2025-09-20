using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] protected GameObject firePoint;
    [SerializeField] protected int projectilePoolSize = 10;
    [SerializeField] protected float cooldownBetweenShots;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected AudioSource audioSource;

    protected Queue<GameObject> projectilePool;
    protected bool canShoot;

    public bool IsTriggerPulled { get; set; }

    private void OnDestroy()
    {
        AudioManager am = FindAnyObjectByType<AudioManager>();
        if (am != null)
        {
            am.ChangeAudioState.RemoveListener(OnAMStateChange);
            Debug.LogWarning($"{gameObject.name} unsubscribed from AM!");
        }
    }

    protected virtual void Awake()
    {
        projectilePool = new();
        canShoot = true;
        IsTriggerPulled = false;
    }

    protected virtual GameObject TakeFromPool()
    {
        Debug.LogWarning("This is the Base version of TakeFromPool!");
        return null;
    }

    public void SubscribeToEvents()
    {
        AudioManager am = FindAnyObjectByType<AudioManager>();
        if (am != null)
        {
            am.ChangeAudioState.AddListener(OnAMStateChange);
            //Debug.LogWarning($"{gameObject.name} subscribed to AM!");
        }
        else
        {
            throw new System.ArgumentNullException($"Gun ({gameObject.name}) couldn't find AudioManager!");
        }
    }

    public void InitializeProjectiles()
    {
        if (projectile != null)
        {
            for (int i = 0; i < projectilePoolSize; i++)
            {
                GameObject temp = Instantiate(projectile, firePoint.transform, false);
                projectilePool.Enqueue(temp);

                if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
                {
                    projectileComponent.SetParentGun(gameObject);
                }
                else
                {
                    Debug.LogError("No projectile components found for current projectile!");
                }

                temp.SetActive(false);
            }
            //Debug.LogWarning($"{gameObject.name}: Projectiles initialized!");
        }
        else
            Debug.LogError($"NO PROJECTILES FOR THE GUN! Gun name: {gameObject.name}");
    }

    public virtual void Shoot()
    {
        if (!canShoot)
            return;

        if (projectilePool.Count < 1)
            return;

        //Debug.LogWarning("ÁÀÌ!");
        GameObject temp = TakeFromPool();

        if (temp == null)
            return;

        temp.SetActive(true);
        if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
            projectileComponent.FireProjectile();
        else
            Debug.LogError("Gun -> Shoot -> No projectile component for current projectile!");

        audioSource.Play();
        StartCoroutine(ShootCooldown());
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        projectilePool.Enqueue(objectToReturn);
        objectToReturn.SetActive(false);

        objectToReturn.transform.parent = firePoint.transform;
        objectToReturn.transform.localEulerAngles = Vector3.zero;
        objectToReturn.transform.localPosition = Vector3.zero;
        //Debug.Log($"Returned projectile to pool! Pool size: {projectilePool.Count}");
    }

    public void OnAMStateChange(bool soundOn)
    {
        //Debug.LogWarning($"{gameObject.name}: OnAMStateChange callback -> soundOn value: {soundOn}");
        if (soundOn)
            audioSource.mute = false;
        else 
            audioSource.mute = true;
    }

    protected IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownBetweenShots);
        canShoot = true;
    }
}
