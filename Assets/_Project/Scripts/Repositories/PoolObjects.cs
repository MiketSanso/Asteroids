using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameScene.Repositories
{
    public class PoolObjects<T>
    {
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private List<T> _active = new List<T>();

        public Queue<T> Pool { get; private set; } = new Queue<T>();
        
        public PoolObjects(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;

            if (preloadFunc == null)
            {
                Debug.LogError("Preload function is null");
                return;
            }
            
            for (int i = 0; i < preloadCount; i++)
                Return(preloadFunc());
        }

        public T Get()
        {
            T item = Pool.Count > 0 ? Pool.Dequeue() : _preloadFunc();
            _getAction(item);
            _active.Add(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            Pool.Enqueue(item);
            _active.Remove(item);
        }

        public void ReturnAll()
        {
            foreach (T item in _active.ToArray())
                Return(item);
        }
    }
}