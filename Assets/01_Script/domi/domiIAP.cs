using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Core;
using OneStore.Purchasing;
using OneStore.Auth;

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

    // 최종 결제 완료
    public void OnConsumeSucceeded(PurchaseData purchase)
    {
        // throw new System.NotImplementedException();
    }

    public void OnManageRecurringProduct(IapResult iapResult, PurchaseData purchase, RecurringAction action)
    {
        throw new System.NotImplementedException();
    }

    public void OnNeedLogin()
    {
        new OneStoreAuthClientImpl().LaunchSignInFlow(signInResult => {});
    }

    public void OnNeedUpdate()
    {
        m_StoreContoller.LaunchUpdateOrInstallFlow((IapResult result) => {});
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
        // m_StoreContoller.NowClose();
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
            print("[domiIAP - onestore] Finish Pay");
        };

        int i = 0;
        foreach (var item in purchases)
        {
            NetworkCore.Send("store.buy_onestore", new ShopPacket_onestore() {
                id = item.ProductId,
                index = i,
                token = item.PurchaseToken
            });
            print("[domiIAP - onestore] Purchase Check Server / "+ item.ProductId + " / "+ i);
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

    private void Start() {
        // 소비하지 않은게 있는지 확인하는거임
        m_StoreContoller.QueryPurchases(ProductType.INAPP);
    }

    private void OnDestroy() {
        NetworkCore.EventListener.Remove("store.complete");
        m_StoreContoller?.EndConnection();
    }

    public void ShowProduct(string id, UnityAction<bool> cb) {
        var purchaseFlowParams = new PurchaseFlowParams.Builder()
          .SetProductId(id)                // mandatory
          .SetProductType(ProductType.INAPP)            // mandatory
          .Build();
        cacheCallback = cb;
        m_StoreContoller.Purchase(purchaseFlowParams);
        m_StoreContoller.NowClose();
    }

    public void AddProduct(string id) {}
}
