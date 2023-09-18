using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StoreUISCI : MonoBehaviour
{
    UIDocument _uiDocument;
    Button _storeBtn;
    Button _gachaBtn;
    VisualElement _errorPanel;
    Button _acceptBtn;

    VisualElement _root;
    public PurchaseUI phu;
    
    void OnEnable()
    {
        _uiDocument =GetComponent<UIDocument>();
        _root = _uiDocument.rootVisualElement;

        _storeBtn = _root.Q<Button>("StoreBtn");
        _gachaBtn = _root.Q<Button>("GachaBtn");
        _errorPanel = _root.Q<VisualElement>("error-panel");
        _acceptBtn = _root.Q<Button>("accept-btn");
        Button  _chargingBtn = _root.Q < Button>("ChargingBtn");
        _chargingBtn.clicked += () => phu.ActivePanel(true);
        _acceptBtn.clicked += () => Preparingforimplementation(false);
        
        _storeBtn.clicked += () => Preparingforimplementation(true);
        _gachaBtn.clicked += () => LoadManager.LoadScene(SceneEnum.Gacha);
    }

    private void Preparingforimplementation(bool isActive)
    {
        if (isActive)
            _errorPanel.AddToClassList("on");
        else
            _errorPanel.RemoveFromClassList("on");
    }

    void SceneLoad(string input)
    {
        SceneManager.LoadScene(input);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
