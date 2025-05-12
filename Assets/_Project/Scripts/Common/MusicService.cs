using Cysharp.Threading.Tasks;
using GameSystem.Common.LoadAssetSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Common
{
    public class MusicService : MonoBehaviour
    {
        private const string MAIN_MUSIC = "MainAudio";
        private const string DESTROY_SOUND = "ExplosionAudio";
        private const string SHOT_SOUND = "ShotAudio";
        
        [SerializeField] private AudioSource _audioSource;
        
        private AudioClip _destroySound;
        private AudioClip _shotSound;
        private AddressablePrefabLoader<AudioClip> _sound;

        [Inject]
        private void Construct(AddressablePrefabLoader<AudioClip> sound)
        {
            _sound = sound;
        }
        
        private void Start()
        {
            LoadMusic().Forget();
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

        private async UniTask LoadMusic()
        {
            AudioClip mainMusic = await _sound.Load(MAIN_MUSIC);
            _destroySound = await _sound.Load(DESTROY_SOUND);
            _shotSound = await _sound.Load(SHOT_SOUND);
            
            if (mainMusic != null)
            {
                _audioSource.clip = mainMusic;
                _audioSource.Play();
            }
        }
    }
}