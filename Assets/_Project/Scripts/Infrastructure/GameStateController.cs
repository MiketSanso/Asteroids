using System;
using GameScene.Repositories;

namespace GameScene.Level
{
    public class GameStateController
    {
        public event Action OnRestart;
        public event Action OnFinish;
        public event Action OnCloseGame;
        
        public void CloseGame()
        {
            OnCloseGame?.Invoke();
        }
        
        public void RestartGame()
        {
            OnRestart?.Invoke();
        }
        
        public void FinishGame()
        {
            OnFinish?.Invoke();
        }
    }
}