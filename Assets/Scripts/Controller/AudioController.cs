using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float fxVolume = 0.5f;

    public AudioClip _playerShootSFX;
    public AudioClip _enemyShootSFX;
    public AudioClip _explosionSFX;

    public bool musicEnabled = true;
    public bool fxEnabled = true;

    private AudioSource _musicSource;

    // Start is called before the first frame update
    void Start()
    {
        _musicSource = GetComponent<AudioSource>();
        //PlayBackgroundMusic(backgroundMusic);
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
                //PlayBackgroundMusic(backgroundMusic);
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