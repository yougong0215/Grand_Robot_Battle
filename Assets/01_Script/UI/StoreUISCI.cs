using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StoreUISCI : MonoBehaviour
{
    UIDocument _uiDocument;
    Button _exitBtn;
    Button _storeBtn;
    Button _gachaBtn;
    VisualElement _errorPanel;
    Button _acceptBtn;

    VisualElement _root;

    
    void OnEnable()
    {
        _uiDocument =GetComponent<UIDocument>();
        _root = _uiDocument.rootVisualElement;
        _exitBtn = _root.Q<Button>("ExitBtn");
        _storeBtn = _root.Q<Button>("StoreBtn");
        _gachaBtn = _root.Q<Button>("GachaBtn");
        _errorPanel = _root.Q<VisualElement>("error-panel");
        _acceptBtn = _root.Q<Button>("accept-btn");
        _acceptBtn.clicked += () => Preparingforimplementation(false);


        _exitBtn.clicked += () => SceneLoad("Menu");
        _storeBtn.clicked += () => Preparingforimplementation(true);
        _gachaBtn.clicked += () => SceneLoad("Gacha");
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
