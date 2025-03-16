using TMPro;
using UnityEngine;
using GameScene.Entities.Player;

namespace GameScene.Level
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _angleOfRotations;
        [SerializeField] private TMP_Text _instantaneousSpeed;
        [SerializeField] private TMP_Text _countLaserCharges;
        [SerializeField] private TMP_Text _timeRollbackLaser;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            _coordinates.text = $"Coordinates: {_player.transform.position}";
            _angleOfRotations.text = $"Rotation: {Mathf.Round(_player.transform.rotation.eulerAngles.z)}°";

            float speed = Mathf.Round(Mathf.Abs(_player.GetComponent<Rigidbody2D>().linearVelocity.magnitude) * 100) / 100f;
            _instantaneousSpeed.text = $"MomentSpeed: {speed}";
            //_countLaserCharges
            //_timeRollbackLaser
        }
    }
}