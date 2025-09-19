using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource amAudioSource;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        amAudioSource.Play();
    }

    public void MuteOrUnmuteAudioSource(bool mute)
    {
        if (mute)
            amAudioSource.mute = true;
        else
            amAudioSource.mute = false;
    }
}
