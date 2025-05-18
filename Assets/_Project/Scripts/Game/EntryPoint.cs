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
        private readonly EndGamePresenter _endGamePresenter; 
        
        private readonly AddressablePrefabLoader<GameObject> _loadAddressablePrefab;

        public EntryPoint(AddressablePrefabLoader<GameObject> loadAddressablePrefab, 
            IInstantiator instantiator,
            EndGamePresenter endGamePresenter)
        {
            _loadAddressablePrefab = loadAddressablePrefab;
            Instantiator = instantiator;
            _endGamePresenter = endGamePresenter;
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
            
            _endGamePresenter.Initialize(canvas.GetComponent<EndPanelView>());

            canvas.worldCamera = Camera.main;
        }
    }
}