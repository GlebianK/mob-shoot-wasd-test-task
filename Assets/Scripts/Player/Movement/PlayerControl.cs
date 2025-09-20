using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float playerSideSpeed = 5.5f;
    [SerializeField] private float playerRotationSpeed = 5.5f;

    private Vector2 direction;
    private Vector2 dragOrTouchDelta;

    #region UNITY METHODS
    private void Start()
    {
        direction = Vector2.zero;
        dragOrTouchDelta = Vector2.zero;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }
    #endregion

    #region CUSTOM PRIVATE METHODS
    private void MovePlayer()
    {
        if (direction != Vector2.zero)
            rb.linearVelocity = direction * (playerSideSpeed * Time.deltaTime);
        else
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 0.5f);
    }

    private void RotatePlayer()
    {
        if (GameManager.Instance.CurrentDeviceType == DeviceType.Handheld)
        {
            if (dragOrTouchDelta != Vector2.zero)
            {
                float angleInDergrees = dragOrTouchDelta.y != 0 ? dragOrTouchDelta.y : dragOrTouchDelta.x;
                transform.Rotate(Vector3.forward, angleInDergrees * playerRotationSpeed * Time.deltaTime);
            }
        }
        else if (GameManager.Instance.CurrentDeviceType == DeviceType.Desktop)
        {
            Vector2 rotationDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
    }
    #endregion

    #region INPUT SYSTEM CALLBACKS
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
            //Debug.Log($"OnMove callback. Value = {direction}");
        }
        else if (context.canceled)
        {
            direction = Vector2.zero;
            //Debug.LogWarning("OnMove callback. Movement canceled!");
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        dragOrTouchDelta = context.ReadValue<Vector2>();
        //Debug.Log($"OnLook callback. Value = {dragOrTouchDelta}");
        //DevCanvas.Instance.UpdateIT2($"delta: {dragOrTouchDelta.x.ToString("F1")}; {dragOrTouchDelta.y.ToString("F1")}");
        //RotatePlayer();
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.LogWarning("Quit button (Esc) was pressed!");
            GameManager.Instance.QuitApp();
        }
    }
    #endregion

    #region OTHER CALLBACKS (Health system, UI related, etc)
    public void OnDied()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnPressForward()
    {
        direction = Vector2.right;
    }

    public void OnPressBackward()
    {
        direction = Vector2.left;
    }

    public void OnStopMovement()
    {
        direction = Vector2.zero;
    }
    #endregion
}
