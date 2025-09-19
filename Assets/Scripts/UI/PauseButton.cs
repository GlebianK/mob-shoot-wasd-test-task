using UnityEngine;

public class PauseButton : MonoBehaviour
{
    // TODO: добавить Invoke паузы через GameplayCanvas, чтобы вызвать панель паузы?

    public void OnPressPause()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
