using GameScene.Entities.Asteroid;
using UnityEngine;
using GameScene.Entities.Player;

namespace GameScene.Entities.A
{
    public class UFO : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public Player Player;

        private void Update()
        {
            if (Player != null)
            {
                Vector3 direction = Player.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * _speed * Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyDestroyer asteroidDestroyer))
            {
                Deactivate();
                asteroidDestroyer.Destroy();
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public UFO Create(Transform transformSpawn, Player player)
        {
            UFO ufo = Instantiate(this, transformSpawn.position, Quaternion.identity);
            ufo.Player = player;
            return ufo;
        }
    }
}
