using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.Purchasing;

public class domiTestShop : MonoBehaviour, IStoreListener
{
    IStoreController m_StoreContoller;

    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct("domi_test", ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success");
        m_StoreContoller = controller;

        // test
        m_StoreContoller.InitiatePurchase("domi_test");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        print("결제 완료");
        //Retrive the purchased product
        var product = purchaseEvent.purchasedProduct;

        print("Purchase Complete" + product.definition.id);

        return PurchaseProcessingResult.Complete;
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
}
