using TMPro;
using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Game
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _angleOfRotations;
        [SerializeField] private TMP_Text _instantaneousSpeed;
        [SerializeField] private TMP_Text _countLaserCharges;
        [SerializeField] private TMP_Text _timeRollbackLaser;

        private PlayerUI _playerUI;
        private Laser _laser;

        [Inject]
        private void Construct(Laser laser, PlayerUI playerUI)
        {
            _laser = laser;
            _playerUI = playerUI;
        }
        
        private void Update()
        {
            float speed = Mathf.Round(Mathf.Abs(_playerUI.GetComponent<Rigidbody2D>().linearVelocity.magnitude) * 100) / 100f;
            
            _instantaneousSpeed.text = $"Moment speed: {speed}";
            _coordinates.text = $"Coordinates: {_playerUI.transform.position}";
            _angleOfRotations.text = $"Rotation: {Mathf.Round(_playerUI.transform.rotation.eulerAngles.z)}°";
            _countLaserCharges.text = $"Count shoots laser: {_laser.CountShotsLaser}";
            _timeRollbackLaser.text = $"Time rollback laser: {_laser.TimeRechargeLaser}";
        }
    }
}