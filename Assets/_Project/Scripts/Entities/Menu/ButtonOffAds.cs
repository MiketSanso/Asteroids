using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ButtonOffAds : MonoBehaviour, IStoreListener
{
    public const string NO_ABS_PRODUCT_ID = "com.yourcompany.yourgame.noads";

    public Button noAdsButton;
    public Text statusText;
    
    private IStoreController storeController;
    private IExtensionProvider extensions;
    
    private void Start()
    {
        InitializePurchasing();
        UpdateUI();
    }
    
    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(NO_ABS_PRODUCT_ID, ProductType.NonConsumable);
        
        UnityPurchasing.Initialize(this, builder);
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        storeController = controller;
        extensions = extension;
        statusText.text = "Магазин готов";
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        statusText.text = "Ошибка инициализации: " + error;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        statusText.text = "Ошибка инициализации: " + error;
    }

    public void BuyNoAds()
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
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == NO_ABS_PRODUCT_ID)
        {
            // Сохраняем факт покупки
            PlayerPrefs.SetInt("NoAdsPurchased", 1);
            UpdateUI();
            statusText.text = "Реклама отключена! Спасибо!";
        }
        
        return PurchaseProcessingResult.Complete;
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        statusText.text = "Ошибка покупки: " + failureReason;
    }
    
    void UpdateUI()
    {
        bool noAdsPurchased = PlayerPrefs.GetInt("NoAdsPurchased", 0) == 1;
        noAdsButton.interactable = !noAdsPurchased;
    }
}
