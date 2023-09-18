using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Core;
using UnityEngine.Purchasing;

struct ShopPacket {
    public string payload;
    public string id;
}

public class domiIAP : MonoBehaviour, IStoreListener
{
    IStoreController m_StoreContoller;
    Product cacheProduct;
    UnityAction<bool> cacheCallback;

    List<string> products = new();

    public void AddProduct(string id) => products.Add(id);

    public void ShowProduct(string id, UnityAction<bool> cb) {
        cacheCallback = cb;
        m_StoreContoller.InitiatePurchase(id);
    }

    void IAP_Complete(LitJson.JsonData data) {
        if (!(bool)data) {
            Debug.LogWarning("[domiIAP] Server Check Worng");
            cacheCallback.Invoke(false);
            cacheCallback = null;
            return;
        }

        print("[domiIAP] Finish Pay");
        m_StoreContoller.ConfirmPendingPurchase(cacheProduct);
        print("[domiIAP] ConfirmPendingPurchase");
        cacheCallback.Invoke(true);
        cacheCallback = null;
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (var item in products)
            builder.AddProduct(item, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("store.complete");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("[domiIAP] Init Success");
        NetworkCore.EventListener["store.complete"] = IAP_Complete;
        m_StoreContoller = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("[domiIAP] Init Fail");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("[domiIAP] Init Fail / "+ message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        cacheCallback?.Invoke(false);
        cacheCallback = null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = cacheProduct = purchaseEvent.purchasedProduct;
        var data = LitJson.JsonMapper.ToObject(product.receipt);

        print("[domiIAP] Purchase Check Server / "+ product.definition.id);
        NetworkCore.Send("store.buy", new ShopPacket() { payload = (string)data["Payload"], id = product.definition.id });

        return PurchaseProcessingResult.Pending;
    }
}
