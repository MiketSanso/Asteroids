using System;
using Zenject;

namespace GameScene.Common
{
    public class GameEndController : IInitializable, IDisposable
    {
        public event Action OnRestart;
        public event Action OnResume;

        private readonly IGameEndEventer _gameEndEventer;

        public GameEndController(IGameEndEventer gameEndEventer)
        {
            _gameEndEventer = gameEndEventer;
        }

        public void Initialize()
        {
            _gameEndEventer.OnRestart += Restart;
            _gameEndEventer.OnResume += Resume;
        }

        public void Dispose()
        {
            _gameEndEventer.OnRestart -= Restart;
            _gameEndEventer.OnResume -= Resume;
        }

        private void Restart()
        {
            OnRestart?.Invoke();
        }
        
        private void Resume()
        {
            OnResume?.Invoke();
        }
    }
}