using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerUpHandler
{
    private PlayerShoot playerShoot;

    private void Start()
    {
        PlayerShoot playerControl = FindAnyObjectByType<PlayerShoot>();

        if (playerControl == null)
            throw new System.ArgumentNullException($"{gameObject.name}: No play control component!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerShoot.StopShoot();
    }
}
