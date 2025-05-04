using _Project.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;

public class ButtonOffAds : MonoBehaviour, IStoreListener
{
    private const string NO_ABS_PRODUCT_ID = "com.yourcompany.yourgame.noads";

    public Button noAdsButton;
    public TMP_Text statusText;
    
    private IStoreController storeController;
    private SaveService _saveService;

    [Inject]
    private void Construct(SaveService saveService)
    {
        _saveService = saveService;
    }
    
    private void Start()
    {
        noAdsButton.onClick.AddListener(BuyNoAds);
        InitializePurchasing();
        UpdateUI();
    }

    private void OnDestroy()
    {
        noAdsButton.onClick.RemoveListener(BuyNoAds);
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == NO_ABS_PRODUCT_ID)
        {
            _saveService.Data.IsAdsOff = true;
            _saveService.Save().Forget();
            UpdateUI();
            statusText.text = "Реклама отключена! Спасибо!";
        }
        
        return PurchaseProcessingResult.Complete;
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        statusText.text = "Ошибка покупки: " + failureReason;
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        storeController = controller;
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        statusText.text = "Ошибка инициализации: " + error;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        statusText.text = "Ошибка инициализации: " + error;
    }
    
    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(NO_ABS_PRODUCT_ID, ProductType.NonConsumable);
        
        UnityPurchasing.Initialize(this, builder);
    }

    private void BuyNoAds()
    {
        Product product = storeController.products.WithID(NO_ABS_PRODUCT_ID);
        
        if (product != null && product.availableToPurchase)
        {
            storeController.InitiatePurchase(product);
        }
        else
        {
            statusText.text = "Продукт недоступен";
        }
    }
    
    private void UpdateUI()
    {
        noAdsButton.interactable = !_saveService.Data.IsAdsOff;
    }
}
