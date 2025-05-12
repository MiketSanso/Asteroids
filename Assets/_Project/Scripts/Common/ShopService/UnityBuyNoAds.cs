using System;
using GameScene.Common.DataSaveSystem;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;

namespace GameScene.Common
{
    public class UnityBuyNoAds : IUnityAdsInitializationListener, IStoreListener, IBuyNoAdsService
    {
        private const string NO_ABS_PRODUCT_ID = "com.yourcompany.yourgame.noads";
        
        public event Action OnDisableAds;
        public event Action<string> OnSendInfo;
        
        private string _gameId;
        private IStoreController _storeController;
        private readonly SaveService _saveService;
        private readonly string _androidGameId = "5833054";
        private readonly bool _testMode = true;
    
        private UnityBuyNoAds(SaveService saveService)
        {
            _saveService = saveService;
        }
    
        public void Initialize()
        {
            Advertisement.debugMode = _testMode;
            Advertisement.Initialize(_androidGameId, _testMode, this);
    
            InitializePurchasing();
        }
    
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (args.purchasedProduct.definition.id == NO_ABS_PRODUCT_ID)
            {
                _saveService.Data.IsAdsOff = true;
                _saveService.Save();
                OnDisableAds?.Invoke();
                OnSendInfo?.Invoke("Реклама отключена! Спасибо!");
            }
    
            return PurchaseProcessingResult.Complete;
        }
    
        private void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(NO_ABS_PRODUCT_ID, ProductType.NonConsumable);
    
            UnityPurchasing.Initialize(this, builder);
        }
    
        public void BuyNoAds()
        {
            Product product = _storeController.products.WithID(NO_ABS_PRODUCT_ID);
    
            if (product != null && product.availableToPurchase)
            {
                _storeController.InitiatePurchase(product);
            }
            else
            {
                OnSendInfo?.Invoke("Продукт недоступен");
            }
        }
    
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Shop initialization complete.");
        }
    
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Shop Initialization Failed: {error.ToString()} - {message}");
        }
    
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            OnSendInfo?.Invoke("Ошибка покупки.");
        }
    
        public void OnInitialized(IStoreController controller, IExtensionProvider extension)
        {
            _storeController = controller;
        }
    
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Ошибка инициализации: " + error);
        }
    
        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("Ошибка инициализации: " + error);
        }
    }
}