using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameScene.Repositories
{
    public class PoolObjects<T>
    {
        private readonly List<T> _active = new List<T>();
        private readonly Func<UniTask<T>> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;

        public Queue<T> Pool { get; private set; } = new Queue<T>();
        
        public PoolObjects(Func<UniTask<T>> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;

            if (preloadFunc == null)
            {
                Debug.LogError("Preload function is null");
                return;
            }

            Preload(preloadCount).Forget();
        }
        
        public async UniTask<T> Get()
        {
            T item = Pool.Count > 0 ? Pool.Dequeue() : await _preloadFunc();
            _getAction(item);
            _active.Add(item);

            return item;
        }

        public void ReturnAll()
        {
            foreach (T item in _active.ToArray())
                Return(item);
        }
        
        private void Return(T item)
        {
            _returnAction(item);
            Pool.Enqueue(item);
            _active.Remove(item);
        }
        
        private async UniTask Preload(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T item = await _preloadFunc();
                Return(item);
            }
        }
    }
}