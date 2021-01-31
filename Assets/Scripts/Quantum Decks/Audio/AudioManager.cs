using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Quantum_Decks.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer Mixer;

        private static AudioManager _instance;

        public static AudioManager Instance => _instance;

        private AudioSource[] _musicSources;
        private AudioSource[] _sfxSources;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            _musicSources = GetComponentsInChildren<AudioSource>().Where(x => x.loop).ToArray();
            _sfxSources = GetComponentsInChildren<AudioSource>().Where(x => !x.loop).ToArray();
        }

        public void SetMusicMute(bool muted)
        {
            foreach (var musicSource in _musicSources)
            {
                musicSource.mute = muted;
            }
        }

        public void SetSfxMute(bool muted)
        {
            foreach (var musicSource in _sfxSources)
            {
                musicSource.mute = muted;
            }
        }

        public void SwitchToBattle()
        {
            Mixer.FindSnapshot("BossMusic").TransitionTo(5);
        }

        public void SwitchToMain()
        {
            Mixer.FindSnapshot("MainMusic").TransitionTo(5);
        }

        public void PlaySfx(SFX sfx)
        {
            try
            {
                _sfxSources[(int) sfx].Play();
            }
            catch (Exception)
            {
                Debug.LogWarning($"The audio No. ${sfx} couldn't be played");
            }
        }
    }
}