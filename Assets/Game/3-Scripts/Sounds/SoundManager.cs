using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Variables ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Private Variables --------------------------- //
    // Singleton instance
    private static SoundManager instance;

    [Header("Main audio parameters")]
    // Audio mixer for controlling volume levels
    [Tooltip("Main audio mixer for controlling volume levels")]
        [SerializeField] private AudioMixer audioMixer;
    [Tooltip("SFX mixer group")]
        [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [Tooltip("Music mixer group")]
        [SerializeField] private AudioMixerGroup musicMixerGroup;

    [Space(5)]

    [Tooltip("Audio mixer parameter name for master volume")]
        [SerializeField] private string masterVolumeParameter = "MainVolume";
    [Tooltip("Audio mixer parameter name for SFX volume")]
        [SerializeField] private string sfxVolumeParameter = "SFXVolume";
    [Tooltip("Audio mixer parameter name for music volume")]
        [SerializeField] private string musicVolumeParameter = "MusicVolume";

    // Audio sources
    private AudioSource bgMusicSource;
    private AudioSource sourceUI_SFX;
    private List<AudioSource> sourcesSFX; // SFX sources pool

    [Header("Audio Source Settings")]
    // Initial value numbers
    [Tooltip("How many initial SFX sources to create")]
        [SerializeField] private int initialSFXSources = 5;

    [Space(5)]

    [Tooltip("Base Volume for background music on the Audio Source")]
        [SerializeField] private float bgMusicVolume = 1.0f;
    [Tooltip("Base Volume for UI SFX on the Audio Source")]
        [SerializeField] private float uiSFXVolume = 1.0f;
    [Tooltip("Base Volume for SFX on the Audio Source")]
        [SerializeField] private float sfxVolume = 1.0f;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Unity Methods ----------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // Create a singleton instance of the SceneManager
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        // Create audio sources for background music, UI SFX, and SFX
        // Create audio source for background music
        bgMusicSource = gameObject.AddComponent<AudioSource>();
        bgMusicSource.playOnAwake = false;
        bgMusicSource.loop = true;
        bgMusicSource.outputAudioMixerGroup = musicMixerGroup;
        bgMusicSource.volume = bgMusicVolume;

        // Create audio source for UI SFX
        sourceUI_SFX = gameObject.AddComponent<AudioSource>();
        sourceUI_SFX.playOnAwake = false;
        sourceUI_SFX.outputAudioMixerGroup = sfxMixerGroup;
        sourceUI_SFX.volume = uiSFXVolume;

        // Create a list of audio sources for SFX
        sourcesSFX = new List<AudioSource>();
        for (int i = 0; i < initialSFXSources; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;
            sfxSource.volume = sfxVolume;
            sourcesSFX.Add(sfxSource);
        }
    }

    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Public Methods ----------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Getters / Setters --------------------------- //
    // Get the instance of the SoundManager
    static public SoundManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("SoundManager instance is null. Make sure there is a SoundManager in the scene and to call it after the Awake.");
        }
        return instance;
    }


    // Master Volume
    public void SetMasterVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat(masterVolumeParameter, -80f);
        }
        else
        {
            audioMixer.SetFloat(masterVolumeParameter, Mathf.Log10(volume) * 20);
        }
    }

    public float GetMasterVolume()
    {
        float volume;
        audioMixer.GetFloat(masterVolumeParameter, out volume);
        return Mathf.Pow(10, volume / 20);
    }


    // Music Volume
    public void SetMusicVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat(musicVolumeParameter, -80f);
        }
        else
        {
            audioMixer.SetFloat(musicVolumeParameter, Mathf.Log10(volume) * 20);
        }
    }

    public float GetMusicVolume()
    {
        float volume;
        audioMixer.GetFloat(musicVolumeParameter, out volume);
        return Mathf.Pow(10, volume / 20);
    }


    // SFX Volume
    public void SetSFXVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat(sfxVolumeParameter, -80f);
        }
        else
        {
            audioMixer.SetFloat(sfxVolumeParameter, Mathf.Log10(volume) * 20);
        }
    }

    public float GetSFXVolume()
    {
        float volume;
        audioMixer.GetFloat(sfxVolumeParameter, out volume);
        return Mathf.Pow(10, volume / 20);
    }

    // --------------------------- Main Methods --------------------------- //
    // Play background music if the clip is not null and is different from the current clip
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (bgMusicSource == null) return;


        // If the level has an assigned music and it's not already playing
        if (clip != null && bgMusicSource.clip != clip)
        {
            bgMusicSource.clip = clip;
            bgMusicSource.loop = true; // Looped by default on play
            bgMusicSource.Play();
        }
    }

    // Stop the background music
    public void StopBackgroundMusic()
    {
        if (bgMusicSource == null) return;

        bgMusicSource.Stop();
    }

    // Loop the background music
    public void LoopBackgroundMusic (bool loop)
    {
        if (bgMusicSource == null) return;

        bgMusicSource.loop = loop;
    }

    // Play a UI sound effect if the clip is not null
    public void PlayUISFX(AudioClip clip)
    {
        if (clip != null)
        {
            sourceUI_SFX.PlayOneShot(clip);
        }
    }

    //
    public AudioSource PlaySFX(AudioClip clip)
    {
        if (clip == null) return null;

        foreach (AudioSource source in sourcesSFX)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                return source; 
            }
        }

        // Create new source if all existing sources are playing
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        sfxSource.volume = sfxVolume;
        sourcesSFX.Add(sfxSource);


        sfxSource.clip = clip;
        sfxSource.Play();
        return sfxSource;
    }
}
