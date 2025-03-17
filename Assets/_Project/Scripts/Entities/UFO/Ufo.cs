using GameScene.Interfaces;
using UnityEngine;
using GameScene.Entities.Player;
using System;

namespace GameScene.Entities.UFOs
{
    public class Ufo : MonoBehaviour, IDestroyableEnemy
    {
        public event Action<int> OnDestroyed;
        
        [SerializeField] private float _speed;
        [SerializeField] private int _scoreSize;
        
        private PlayerUI Player;

        private void Update()
        {
            if (Player != null)
            {
                Vector3 direction = Player.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * _speed * Time.deltaTime;
            }
        }

        public void Destroy(bool isPlayer)
        {
             Deactivate();
            
             if (!isPlayer) 
                 OnDestroyed?.Invoke(_scoreSize);
        }

        public void Activate(Transform transformSpawn)
        {
            transform.position = transformSpawn.position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public Ufo Create(Transform transformSpawn, Transform transformParent, PlayerUI player)
        {
            Ufo ufo = Instantiate(this, transformSpawn.position, Quaternion.identity, transformParent);
            ufo.Player = player;
            return ufo;
        }
    }
}
