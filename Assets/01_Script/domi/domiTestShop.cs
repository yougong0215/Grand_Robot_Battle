using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.Purchasing;

public class domiTestShop : MonoBehaviour, IStoreListener
{
    IStoreController m_StoreContoller;
    Product cacheProduct;

    private void Awake() {
        NetworkCore.EventListener["store.complete"] = IAP_Complete;
    }
    private void OnDestroy() {
        NetworkCore.EventListener.Remove("store.complete");
    }

    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct("domi_test2", ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success");
        m_StoreContoller = controller;

        // test
        m_StoreContoller.InitiatePurchase("domi_test2");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        print("결제 완료");
        //Retrive the purchased product
        var product = cacheProduct = purchaseEvent.purchasedProduct;
        var data = LitJson.JsonMapper.ToObject(product.receipt);
        GUIUtility.systemCopyBuffer = product.receipt;

        NetworkCore.Send("store.test", (string)data["Payload"]);
        print("Purchase Complete" + product.definition.id);
        return PurchaseProcessingResult.Pending;
    }

    #region error handeling
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("failed" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("initialize failed" + error + message);
    }



    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("purchase failed" + failureReason);
    }

    #endregion

    void IAP_Complete(LitJson.JsonData data) {
        if (!(bool)data) return;

        print("최종 결제 완료");
        m_StoreContoller.ConfirmPendingPurchase(cacheProduct);
        cacheProduct = null;
    }
}
