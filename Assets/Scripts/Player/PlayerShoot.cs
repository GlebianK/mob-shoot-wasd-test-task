using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.LogWarning("���, ��_��!");
            // TODO: �������� �������
        }
    }
    
}
