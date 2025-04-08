using UnityEngine;
using Zenject;

namespace GameScene.Level
{
    public class EntryPoint : IInitializable
    {
        private readonly Canvas _prefabGameCanvas;
        private readonly IInstantiator _instantiator;

        public EntryPoint(Canvas prefabGameCanvas, IInstantiator instantiator)
        {
            _prefabGameCanvas = prefabGameCanvas;
            _instantiator = instantiator;
        }

        public void Initialize()
        {
            Canvas canvas = _instantiator.InstantiatePrefabForComponent<Canvas>(_prefabGameCanvas);
            canvas.worldCamera = Camera.main;
            
            if (canvas.TryGetComponent(out EndPanel endPanel))
                endPanel.Initialize();
            else
                Debug.LogError("Canvas don't have EndPanel!");
        }
    }
}