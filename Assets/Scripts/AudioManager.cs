using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Manages all sounds in the game. Three types: bgmusic, ambience, fx
/// </summary>
public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public AudioSource BgAudio;
    public AudioSource AmbienceAudio;
    public AudioSource FXAudio;

    public enum AudioType { BgMusic, Ambience, FX };


    // Use this for initialization
    void Awake ()
    {
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Plays given audio clip in the corresponding audiosource
    /// </summary>
    public void Play(AudioType type, AudioClip clip, float volume = 1.0f)
    {
        AudioSource targetAudio = null;

        switch (type)
        {
        case AudioType.BgMusic:
            targetAudio = BgAudio;
            break;
        case AudioType.Ambience:
            targetAudio = AmbienceAudio;
            break;
        case AudioType.FX:
            targetAudio = FXAudio;
            break;
        default:
            targetAudio = FXAudio;
            break;
        }

        targetAudio.clip = clip;
        targetAudio.volume = volume;
        targetAudio.Play();
    }

    public void Play(AudioType type, float volume = 1.0f)
    {
        AudioSource targetAudio = null;

        switch (type)
        {
        case AudioType.BgMusic:
            targetAudio = BgAudio;
            break;
        case AudioType.Ambience:
            targetAudio = AmbienceAudio;
            break;
        case AudioType.FX:
            targetAudio = FXAudio;
            break;
        default:
            targetAudio = FXAudio;
            break;
        }
        
        targetAudio.volume = volume;
        targetAudio.Play();
    }


    /// <summary>
    /// Fades given audio type currently playing
    /// </summary>
    public void Fade(AudioType type, float targetValue, float time)
    {
        AudioSource targetAudio = null;

        switch (type)
        {
        case AudioType.BgMusic:
            targetAudio = BgAudio;
            break;
        case AudioType.Ambience:
            targetAudio = AmbienceAudio;
            break;
        case AudioType.FX:
            targetAudio = FXAudio;
            break;
        default:
            targetAudio = FXAudio;
            break;
        }

        float originalVolume = targetAudio.volume;
        targetAudio.DOFade(targetValue, time).OnComplete(()=>FadeCallback(type, originalVolume));
    }

    private void FadeCallback(AudioType type, float volume)
    {
        AudioSource targetAudio = null;

        switch (type)
        {
        case AudioType.BgMusic:
            targetAudio = BgAudio;
            break;
        case AudioType.Ambience:
            targetAudio = AmbienceAudio;
            break;
        case AudioType.FX:
            targetAudio = FXAudio;
            break;
        default:
            targetAudio = FXAudio;
            break;
        }

        targetAudio.Stop();
        targetAudio.volume = volume;
    }
}
