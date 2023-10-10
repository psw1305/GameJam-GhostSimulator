using UnityEngine;

public class AudioSystem : BehaviourSingleton<AudioSystem>
{
    [Header("Setting")]
    [SerializeField] private AudioSource audioBGM;
    [SerializeField] private AudioSource audioSFX;

    [Header("BGM")]
    public AudioClip stage;

    [Header("SFX")]
    public AudioClip click;
    public AudioClip collision;
    public AudioClip collect;
    public AudioClip gameclear;
    public AudioClip gameover;

    protected override void Awake()
    {
        base.Awake();

        if (PlayerPrefs.HasKey("BGM"))
        {
            this.audioBGM.volume = PlayerPrefs.GetInt("BGM");
        }
        else
        {
            PlayerPrefs.SetInt("BGM", 1);
            this.audioBGM.volume = 1.0f;
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            this.audioSFX.volume = PlayerPrefs.GetInt("SFX");
        }
        else
        {
            PlayerPrefs.SetInt("SFX", 1);
            this.audioSFX.volume = 1.0f;
        }

        PlayBGM(this.stage);
    }

    public void SetBGM(int value)
    {
        PlayerPrefs.SetInt("BGM", value);
        this.audioBGM.volume = PlayerPrefs.GetInt("BGM");
    }

    public void PlayBGM(AudioClip clip)
    {
        this.audioBGM.clip = clip;
        this.audioBGM.PlayOneShot(clip);
    }

    public void SetSFX(int value)
    {
        PlayerPrefs.SetInt("SFX", value);
        this.audioSFX.volume = PlayerPrefs.GetInt("SFX");
    }

    public void PlaySFX(AudioClip clip)
    {
        this.audioSFX.clip = clip;
        this.audioSFX.PlayOneShot(clip);
    }
}
