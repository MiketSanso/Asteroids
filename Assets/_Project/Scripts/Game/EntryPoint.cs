using Cysharp.Threading.Tasks;
using GameScene.Models;
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
        private readonly ScorePresenter _scorePresenter; 
        
        private readonly AddressablePrefabLoader<GameObject> _loadAddressablePrefab;

        public EntryPoint(AddressablePrefabLoader<GameObject> loadAddressablePrefab, 
            IInstantiator instantiator,
            ScorePresenter scorePresenter)
        {
            _loadAddressablePrefab = loadAddressablePrefab;
            Instantiator = instantiator;
            _scorePresenter = scorePresenter;
        }

        public void Initialize()
        {
            LoadCanvas().Forget();
        }

        private async UniTask LoadCanvas()
        {
            Canvas canvas = Instantiator.InstantiatePrefabForComponent<Canvas>(
                await _loadAddressablePrefab.Load(CANVAS_KEY));

            if (!canvas.TryGetComponent(out EndPanelView canvasComp))
            {
                Debug.LogError("Canvas don't have IScoreView");
                return;
            }
            
            _scorePresenter.Initialize(canvas.GetComponent<EndPanelView>());

            canvas.worldCamera = Camera.main;
        }
    }
}