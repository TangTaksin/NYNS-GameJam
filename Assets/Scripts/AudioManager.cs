using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("===================Audio Source=================")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("===================Audio Clip=================")]
    [SerializeField] public AudioClip BGMusic;
    [SerializeField] public AudioClip EndMusic;
    [SerializeField] public AudioClip rotateSFX;
    [SerializeField] public AudioClip pickupSFX;
    [SerializeField] public AudioClip dropSFX;

    

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(BGMusic);
    }

    public void PlayMusic(AudioClip MusicClip)
    {
        musicSource.clip = MusicClip;
        musicSource.Play();

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip SFXClip)
    {
        SFXSource.PlayOneShot(SFXClip);
    }

    
}
