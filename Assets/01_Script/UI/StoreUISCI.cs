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

    VisualElement _root;

    
    void OnEnable()
    {
        _uiDocument =GetComponent<UIDocument>();
        _root = _uiDocument.rootVisualElement;
        _exitBtn = _root.Q<Button>("ExitBtn");
        _storeBtn = _root.Q<Button>("StoreBtn");
        _gachaBtn = _root.Q<Button>("GachaBtn");


        _exitBtn.clicked += () => SceneLoad("Menu");
        _storeBtn.clicked += () => SceneLoad("Store");
        _gachaBtn.clicked += () => SceneLoad("Gacha");
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
