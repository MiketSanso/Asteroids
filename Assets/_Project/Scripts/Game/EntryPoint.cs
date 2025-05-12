using GameSystem.Common.LoadAssetSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Game
{
    public class EntryPoint : IInitializable
    {
        private const string CANVAS_KEY = "Canvas";

        private Canvas _prefabGameCanvas;
        private IInstantiator Instantiator;
        
        private readonly AddressablePrefabLoader<Canvas> _loadAddressablePrefab;

        public EntryPoint(AddressablePrefabLoader<Canvas> loadAddressablePrefab, IInstantiator instantiator)
        {
            _loadAddressablePrefab = loadAddressablePrefab;
            Instantiator = instantiator;
        }

        public async void Initialize()
        {
            Canvas canvas = Instantiator.InstantiatePrefabForComponent<Canvas>(
                await _loadAddressablePrefab.Load(CANVAS_KEY));
            canvas.worldCamera = Camera.main;
            
            if (!canvas.TryGetComponent(out EndPanel endPanel))
                Debug.LogError("Canvas don't have EndPanel!");
        }
    }
}