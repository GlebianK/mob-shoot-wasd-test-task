using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerUpHandler
{
    private PlayerControl playerControl;

    private void Start()
    {
        PlayerControl playerControl = FindAnyObjectByType<PlayerControl>();

        if (playerControl == null)
            throw new System.ArgumentNullException($"{gameObject.name}: No play control component!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerControl.OnStopMovement();
    }
}
