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
        private IInstantiator _instantiator;

        private readonly GamePresenter _gamePresenter;
        private readonly EndGamePresenter _endGamePresenter; 
        private readonly AddressablePrefabLoader<GameObject> _loadAddressablePrefab;

        public EntryPoint(AddressablePrefabLoader<GameObject> loadAddressablePrefab, 
            IInstantiator instantiator,
            EndGamePresenter endGamePresenter,
            GamePresenter gamePresenter)
        {
            _loadAddressablePrefab = loadAddressablePrefab;
            _instantiator = instantiator;
            _endGamePresenter = endGamePresenter;
            _gamePresenter = gamePresenter;
        }

        public void Initialize()
        {
            LoadCanvas().Forget();
        }

        private async UniTask LoadCanvas()
        {
            Canvas canvas = _instantiator.InstantiatePrefabForComponent<Canvas>(
                await _loadAddressablePrefab.Load(CANVAS_KEY));

            if (!canvas.TryGetComponent(out EndPanelView canvasComp))
            {
                Debug.LogError("Canvas don't have IScoreView");
                return;
            }
            
            _endGamePresenter.Initialize(canvas.GetComponent<EndPanelView>());
            _gamePresenter.Initialize(canvas.GetComponent<GameView>());

            canvas.worldCamera = Camera.main;
        }
    }
}