using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Core;
using OneStore.Purchasing;

struct ShopPacket {
    public string payload;
    public string id;
}

struct ShopPacket_onestore
{
    public string token;
    public string id;
    public int index;
}

public class domiIAP : MonoBehaviour, IPurchaseCallback
{
    PurchaseClientImpl m_StoreContoller;
    UnityAction<bool> cacheCallback;

    public void OnAcknowledgeFailed(IapResult iapResult)
    {
        throw new System.NotImplementedException();
    }

    public void OnAcknowledgeSucceeded(PurchaseData purchase, ProductType type)
    {
        throw new System.NotImplementedException();
    }

    public void OnConsumeFailed(IapResult iapResult)
    {
        throw new System.NotImplementedException();
    }

    public void OnConsumeSucceeded(PurchaseData purchase)
    {
        throw new System.NotImplementedException();
    }

    public void OnManageRecurringProduct(IapResult iapResult, PurchaseData purchase, RecurringAction action)
    {
        throw new System.NotImplementedException();
    }

    public void OnNeedLogin()
    {
        throw new System.NotImplementedException();
    }

    public void OnNeedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnProductDetailsFailed(IapResult iapResult)
    {
        throw new System.NotImplementedException();
    }

    public void OnProductDetailsSucceeded(List<ProductDetail> productDetails)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(IapResult iapResult)
    {
        m_StoreContoller.EndConnection();
    }

    public void OnPurchaseSucceeded(List<PurchaseData> purchases)
    {
        NetworkCore.EventListener["store.complete"] = (LitJson.JsonData data) => {
            if (!(bool)data["ok"]) {
                cacheCallback?.Invoke(false);
                return;
            }
            m_StoreContoller.ConsumePurchase(purchases[(int)data["index"]]);
            cacheCallback?.Invoke(true);
            cacheCallback = null;
        };

        int i = 0;
        foreach (var item in purchases)
        {
            NetworkCore.Send("store.buy_onestore", new ShopPacket_onestore() {
                id = item.ProductId,
                index = i,
                token = item.PurchaseToken
            });
            i ++;
        }
    }

    public void OnSetupFailed(IapResult iapResult)
    {
        print("[domiIAP] Onestore Setup Failed / "+iapResult.Message);
    }

    private void Awake() {
        m_StoreContoller = new PurchaseClientImpl("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCSGNai6we8g3wmbVnVeODq0TaWj3XtQRu3IYYYoD/L4Urkwz8J68ToH88IiwmXiOoWIrHnfLyziV+Pov41BA7ucChOaT1L3Cwx0HIAtTn8P4zctZ/7BFdI/H96U27XHbJgBJkkzTjYMk5lYeyiyPiaR/jz9QcraZI2SiSsLZigXwIDAQAB");
        m_StoreContoller.Initialize(this);
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("store.complete");
    }

    public void ShowProduct(string id, UnityAction<bool> cb) {
        var purchaseFlowParams = new PurchaseFlowParams.Builder()
          .SetProductId(id)                // mandatory
          .SetProductType(ProductType.INAPP)            // mandatory
          .Build();
        cacheCallback = cb;
        m_StoreContoller.Purchase(purchaseFlowParams);
    }

    // Product cacheProduct;

    public void AddProduct(string id) {}

    // public void ShowProduct(string id, UnityAction<bool> cb) {
    //     cacheCallback = cb;
    //     m_StoreContoller.InitiatePurchase(id);
    // }

    // void IAP_Complete(LitJson.JsonData data) {
    //     if (!(bool)data) {
    //         Debug.LogWarning("[domiIAP] Server Check Worng");
    //         cacheCallback.Invoke(false);
    //         cacheCallback = null;
    //         return;
    //     }

    //     print("[domiIAP] Finish Pay");
    //     m_StoreContoller.ConfirmPendingPurchase(cacheProduct);
    //     print("[domiIAP] ConfirmPendingPurchase");
    //     cacheCallback.Invoke(true);
    //     cacheCallback = null;
    // }

    // async void Start()
    // {
    //     await UnityServices.InitializeAsync();

    //     var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
    //     foreach (var item in products)
    //         builder.AddProduct(item, ProductType.Consumable);

    //     UnityPurchasing.Initialize(this, builder);
    // }

    // public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    // {
    //     print("[domiIAP] Init Success");
    //     NetworkCore.EventListener["store.complete"] = IAP_Complete;
    //     m_StoreContoller = controller;
    // }

    // public void OnInitializeFailed(InitializationFailureReason error)
    // {
    //     Debug.LogError("[domiIAP] Init Fail");
    // }

    // public void OnInitializeFailed(InitializationFailureReason error, string message)
    // {
    //     Debug.LogError("[domiIAP] Init Fail / "+ message);
    // }

    // public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    // {
    //     cacheCallback?.Invoke(false);
    //     cacheCallback = null;
    // }

    // public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    // {
    //     var product = cacheProduct = purchaseEvent.purchasedProduct;
    //     var data = LitJson.JsonMapper.ToObject(product.receipt);

    //     print("[domiIAP] Purchase Check Server / "+ product.definition.id);
    //     NetworkCore.Send("store.buy", new ShopPacket() { payload = (string)data["Payload"], id = product.definition.id });

    //     return PurchaseProcessingResult.Pending;
    // }
}
