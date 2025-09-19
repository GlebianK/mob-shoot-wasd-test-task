using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Button selfButton;

    private PlayerControl playerControl;

    private void Start()
    {
        PlayerControl playerControl = FindAnyObjectByType<PlayerControl>();

        if (playerControl == null)
            throw new System.ArgumentNullException($"{gameObject.name}: No player control component!");
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        playerControl.OnStopMovement();
    }
}
