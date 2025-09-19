using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP = 25f;
    [SerializeField] private float iFrameDuration = 0f;

    private float currentHP;
    private bool isIFrame;

    public float HP => currentHP;

    public UnityEvent TookDamage;
    public UnityEvent Died;

    private void Awake()
    {
        currentHP = maxHP;
        isIFrame = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void TakeDamage(float damage)
    {
        if (isIFrame)
            return;

        Debug.Log($"{gameObject.name}'s hp before hit: {currentHP}");
        currentHP -= damage;
        TookDamage.Invoke();
        Debug.Log($"{gameObject.name}'s hp after hit: {currentHP}");

        SetIFrame();

        if (currentHP <= 0)
        {
            Debug.LogWarning($"{gameObject.name} died!");
            currentHP = 0;
            Died.Invoke();
        }
    }

    public void SetIFrame()
    {
        StartCoroutine(InitiateIFrameState());
    }

    private IEnumerator InitiateIFrameState()
    {
        isIFrame = true;
        yield return new WaitForSeconds(iFrameDuration);
        isIFrame = false;
    }
}
