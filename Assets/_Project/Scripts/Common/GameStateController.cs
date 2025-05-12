using System;
using System.Collections;
using UnityEngine;

namespace GameScene.Common
{
    public class GameStateController : MonoBehaviour
    {
        public event Action OnResume;
        public event Action OnRestart;
        public event Action OnFinish;
        public event Action OnStart;
        public event Action OnClose;
        
        private IEnumerator Start()
        {
            yield return null;
            OnStart?.Invoke();
        }

        private void OnDestroy()
        {
            OnClose?.Invoke();
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