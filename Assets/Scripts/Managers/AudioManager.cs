using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource amAudioSource;

    public UnityEvent<bool> ChangeAudioState;

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

    public void MuteOrUnmuteAudioSource(bool soundOn)
    {
        if (soundOn)
            amAudioSource.mute = false;
        else
            amAudioSource.mute = true;

        //Debug.LogWarning($"AM soundOn state changed! soundOn value: {soundOn}");
        ChangeAudioState.Invoke(soundOn);
    }
}
