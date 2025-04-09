using UnityEngine;
using Zenject;

namespace GameScene.Entities.Player
{
    public class LaserRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private Laser _laser;

        [Inject]
        private void Construct(Laser laser)
        {
            _laser = laser;
        }
        
        private void Start()
        {
            DisableLine();
            _laser.OnDeactivateLaser += DisableLine;
            _laser.OnActivateLaser += EnableLine;
        }
        
        private void OnDestroy()
        {
            _laser.OnDeactivateLaser -= DisableLine;
            _laser.OnActivateLaser -= EnableLine;
        }

        private void DisableLine()
        {
            _lineRenderer.enabled = false;
        }

        private void EnableLine(Vector2 firstPosition, Vector2 secondPosition)
        {
            _lineRenderer.SetPosition(0, firstPosition);
            _lineRenderer.SetPosition(1, secondPosition);
            _lineRenderer.enabled = true;
        }
    }
}