using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour//, IPointerUpHandler, IPointerDownHandler //, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool isForward;

    private PlayerControl playerControl;

    private void Start()
    {
        PlayerControl playerControl = FindAnyObjectByType<PlayerControl>();

        if (playerControl == null)
            throw new System.ArgumentNullException($"{gameObject.name}: No player control component!");
    }

    public void OnInteractWithUIControls(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isForward)
            {
                playerControl.OnPressForward();
                //DevCanvas.Instance.UpdateIT1("direction: 1; 0");
            }
            else
            {
                playerControl.OnPressBackward();
                //DevCanvas.Instance.UpdateIT1("direction: -1; 0");
            }
        }
        else if (context.canceled)
        {
            playerControl.OnStopMovement();
            //DevCanvas.Instance.UpdateIT1("direction: 0; 0");
        }
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
        {
            if (isForward)
            {
                playerControl.OnPressForward();
                DevCanvas.Instance.UpdateIT1("direction: 1; 0");
            }
            else
            {
                playerControl.OnPressBackward();
                DevCanvas.Instance.UpdateIT1("direction: -1; 0");
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
            playerControl.OnStopMovement();
        DevCanvas.Instance.UpdateIT1("direction: 0; 0");
    }
    */
    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isForward)
        {
            playerControl.OnPressForward();
            DevCanvas.Instance.UpdateIT1("direction: 1; 0");
        }
        else
        {
            playerControl.OnPressBackward();
            DevCanvas.Instance.UpdateIT1("direction: -1; 0");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerControl.OnStopMovement();
        DevCanvas.Instance.UpdateIT1("direction: 0; 0");
    }
    */
}
