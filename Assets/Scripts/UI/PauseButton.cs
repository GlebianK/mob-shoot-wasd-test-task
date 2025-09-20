using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnPressPause()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
