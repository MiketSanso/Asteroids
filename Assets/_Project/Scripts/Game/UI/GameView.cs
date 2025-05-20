using TMPro;
using UnityEngine;
using GameScene.Entities.PlayerSpace;
using Zenject;

namespace GameScene.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _angleOfRotations;
        [SerializeField] private TMP_Text _instantaneousSpeed;
        [SerializeField] private TMP_Text _countLaserCharges;
        [SerializeField] private TMP_Text _timeRollbackLaser;

        private Player _player;
        private Laser _laser;

        [Inject]
        private void Construct(Laser laser, Player player)
        {
            _laser = laser;
            _player = player;
        }
        
        private void Update()
        {
            float speed = Mathf.Round(Mathf.Abs(_player.GetComponent<Rigidbody2D>().linearVelocity.magnitude) * 100) / 100f;
            
            _instantaneousSpeed.text = $"Moment speed: {speed}";
            _coordinates.text = $"Coordinates: {_player.transform.position}";
            _angleOfRotations.text = $"Rotation: {Mathf.Round(_player.transform.rotation.eulerAngles.z)}°";
            _countLaserCharges.text = $"Count shoots laser: {_laser.CountShotsLaser}";
            _timeRollbackLaser.text = $"Time rollback laser: {_laser.TimeRechargeLaser}";
        }
    }
}