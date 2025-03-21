using GameScene.Repositories;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        private BulletFactory _bulletFactory;
        private AsteroidFactory _asteroidFactory;
        private UfoFactory _ufoFactory;
        private ScoreInfo _scoreInfo;

        private void OnDestroy()
        {
            _bulletFactory.Destroy();
            _ufoFactory.Destroy();
            _asteroidFactory.Destroy();
        }
    }
}