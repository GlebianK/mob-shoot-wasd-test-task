using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] private Button volumeButton;
    [Tooltip("0 - vol.off/1 - vol.on"), SerializeField] private Sprite[] volumeSprites;

    private bool soundOn;

    private void Awake()
    {
        soundOn = true;
        volumeButton.image.sprite = volumeSprites[1];
    }

    public void OnPressVolumeButton()
    {
        soundOn = !soundOn;
        //Debug.LogWarning($"Sound button was pressed! soundOn value: {soundOn}");
        AudioManager.Instance.MuteOrUnmuteAudioSource(soundOn);

        if (soundOn)
            volumeButton.image.sprite = volumeSprites[1];
        else
            volumeButton.image.sprite = volumeSprites[0];
    }
}
