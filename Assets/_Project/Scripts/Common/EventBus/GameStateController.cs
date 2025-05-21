using System;
using System.Collections;
using UnityEngine;

namespace GameScene.Common
{
    public class GameStateController : MonoBehaviour
    {
        public event Action OnStart;
        public event Action OnFinish;
        
        private IEnumerator Start()
        {
            yield return null;
            OnStart?.Invoke();
        }

        public void FinishGame()
        {
            OnFinish?.Invoke();
        }
    }
}