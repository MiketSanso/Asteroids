using System;
using System.Collections;
using UnityEngine;

namespace GameScene.Level
{
    public class GameStateController : MonoBehaviour
    {
        public event Action OnRestart;
        public event Action OnFinish;
        public event Action OnStartGame;
        
        private IEnumerator Start()
        {
            yield return null;
            OnStartGame?.Invoke();
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