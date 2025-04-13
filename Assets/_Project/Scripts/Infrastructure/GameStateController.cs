using System;
using System.Collections;
using UnityEngine;

namespace GameScene.Level
{
    public class GameStateController : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return null;
            OnStartGame?.Invoke();
        }

        private void OnDestroy()
        {
            OnCloseGame?.Invoke();
        }

        public event Action OnRestart;
        public event Action OnFinish;
        public event Action OnStartGame;
        public event Action OnCloseGame;

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