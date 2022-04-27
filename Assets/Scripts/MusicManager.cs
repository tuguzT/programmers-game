using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : SingletonMonoBehaviour<MusicManager>
{
    [SerializeField] private AudioClip menuMusic;

    [SerializeField] private AudioClip levelMusic;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        if (audioSource.clip == menuMusic && audioSource.isPlaying) return;

        audioSource.Stop();
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void PlayLevelMusic()
    {
        if (audioSource.clip == levelMusic && audioSource.isPlaying) return;

        audioSource.Stop();
        audioSource.clip = levelMusic;
        audioSource.Play();
    }
}
