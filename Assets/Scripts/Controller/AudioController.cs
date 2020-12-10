using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Controller")]
    [Range(0f, 1f)] public float musicVolume = 0.05f;
    [Range(0f, 1f)] public float fxVolume = 0.1f;

    [Header("Audio Controller Prefabs")]
    public AudioClip _playerShootSFX;
    public AudioClip _playerExplosionSFX;
    public AudioClip _enemyShootSFX;
    public AudioClip _enemyExplosionSFX;
    public AudioClip _backgroudMusicSFX;

    public bool musicEnabled = true;
    public bool fxEnabled = true;

    private AudioSource _musicSource;

    // Start is called before the first frame update
    void Start()
    {
        _musicSource = GetComponent<AudioSource>();

        PlayBackgroundMusic(_backgroudMusicSFX);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (!musicEnabled || !musicClip || !_musicSource)
        {
            return;
        }

        _musicSource.Stop();
        _musicSource.clip = musicClip;
        _musicSource.volume = musicVolume;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    private void UpdateMusic()
    {
        if (_musicSource.isPlaying != musicEnabled)
        {
            if (musicEnabled)
            {
                PlayBackgroundMusic(_backgroudMusicSFX);
            }
            else
            {
                _musicSource.Stop();
            }
        }
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateMusic();
    }

    public void ToggleFX()
    {
        fxEnabled = !fxEnabled;
    }

    public void PlaySound(AudioClip clip)
    {
        if (fxEnabled && clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(fxVolume * musicVolume, 0.05f, 1f));
        }
    }
}