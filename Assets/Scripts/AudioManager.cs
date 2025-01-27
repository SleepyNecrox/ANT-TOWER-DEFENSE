using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip Menu;
    public AudioClip Gameplay;
    public AudioClip Hit;
    public AudioClip ShieldBash;
    public AudioClip Arrow;
    public AudioClip ThrowRock;
    public AudioClip AntDeath;
    public AudioClip CardHover;
    public AudioClip CardPress;
    public AudioClip InfoBoxPopUp;
    public AudioClip Retro;
    public AudioClip Toggle;
    public AudioClip Button;
    public AudioClip Select;

    public static AudioManager Instance;

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
        PlaySceneMusic();
    }

    private void PlaySceneMusic()
    {
        musicSource.clip = Gameplay;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
