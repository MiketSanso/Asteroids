using System;
using System.Collections;
using UnityEngine;

namespace GameScene.Common
{
    public class GameEventBus : MonoBehaviour
    {
        public event Action OnResume;
        public event Action OnRestart;
        public event Action OnFinish;
        public event Action OnStart;
        
        private IEnumerator Start()
        {
            yield return null;
            OnStart?.Invoke();
        }

        public void RestartGame()
        {
            OnRestart?.Invoke();
        }

        public void ResumeGame()
        {
            OnResume?.Invoke();
        }

        public void FinishGame()
        {
            OnFinish?.Invoke();
        }
    }
}