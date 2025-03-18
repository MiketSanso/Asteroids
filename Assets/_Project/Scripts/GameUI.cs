using TMPro;
using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Level
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinates;
        [SerializeField] private TMP_Text _angleOfRotations;
        [SerializeField] private TMP_Text _instantaneousSpeed;
        [SerializeField] private TMP_Text _countLaserCharges;
        [SerializeField] private TMP_Text _timeRollbackLaser;

        [Inject] private PlayerUI _playerUI;
        [Inject] private Shoot _shoot;

        private void Update()
        {
            float speed = Mathf.Round(Mathf.Abs(_playerUI.GetComponent<Rigidbody2D>().linearVelocity.magnitude) * 100) / 100f;
            
            _instantaneousSpeed.text = $"Moment speed: {speed}";
            _coordinates.text = $"Coordinates: {_playerUI.transform.position}";
            _angleOfRotations.text = $"Rotation: {Mathf.Round(_playerUI.transform.rotation.eulerAngles.z)}°";
            _countLaserCharges.text = $"Count shoots laser: {_shoot.CountShotsLaser}";
            _timeRollbackLaser.text = $"Time rollback laser: {_shoot.TimeRechargeLaser}";
        }
    }
}