using GameSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class MusicService : MonoBehaviour
    {
        private const string MAIN_MUSIC = "MainAudio";
        private const string DESTROY_SOUND = "ExplosionAudio";
        private const string SHOT_SOUND = "ShotAudio";
        
        [SerializeField] private AudioSource _audioSource;
        
        private AudioClip _destroySound;
        private AudioClip _shotSound;
        private LoadPrefab<AudioClip> _loadSound;

        [Inject]
        private void Construct(LoadPrefab<AudioClip> loadSound)
        {
            _loadSound = loadSound;
        }
        
        private async void Start()
        {
            AudioClip mainMusic = await _loadSound.LoadPrefabFromAddressable(MAIN_MUSIC);
            _destroySound = await _loadSound.LoadPrefabFromAddressable(DESTROY_SOUND);
            _shotSound = await _loadSound.LoadPrefabFromAddressable(SHOT_SOUND);

            if (mainMusic != null)
            {
                _audioSource.clip = mainMusic;
                _audioSource.Play();
            }
        }

        public void DestroyObject()
        {
            if (_destroySound != null)
                _audioSource.PlayOneShot(_destroySound);
        }
        
        public void Shot()
        {
            if (_shotSound != null)
                _audioSource.PlayOneShot(_shotSound);
        }
    }
}