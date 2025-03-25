using System;

namespace GameScene.Level
{
    public class GameStateController
    {
        public event Action OnRestart;
        public event Action OnFinish;

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