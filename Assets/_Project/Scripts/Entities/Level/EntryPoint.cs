using GameSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Level
{
    public class EntryPoint : IInitializable
    {
        private const string CANVAS_KEY = "Canvas";

        private Canvas _prefabGameCanvas;
        private LoadPrefab<Canvas> _loadPrefab;
        private IInstantiator Instantiator;

        public EntryPoint(LoadPrefab<Canvas> loadPrefab, IInstantiator instantiator)
        {
            _loadPrefab = loadPrefab;
            Instantiator = instantiator;
        }

        public async void Initialize()
        {
            Canvas canvas = Instantiator.InstantiatePrefabForComponent<Canvas>(
                await _loadPrefab.LoadPrefabFromAddressable(CANVAS_KEY));
            canvas.worldCamera = Camera.main;
            
            if (canvas.TryGetComponent(out EndPanel endPanel))
                endPanel.Initialize();
            else
                Debug.LogError("Canvas don't have EndPanel!");
        }
    }
}