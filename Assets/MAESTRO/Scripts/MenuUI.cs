using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;
    private Button _battleBtn;
    private Button _gongBtn;
    private Button _makeBtn;
    private Button _storeBtn;
    private Button _garageBtn;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;
        _battleBtn = _root.Q<Button>("BattleBtn");
        _gongBtn = _root.Q<Button>("GongBtn");
        _makeBtn = _root.Q<Button>("MakeBtn");
        _storeBtn = _root.Q<Button>("StoreBtn");
        _garageBtn = _root.Q<Button>("GarageBtn");

        _makeBtn.clicked += () => SceneLoad("Gacha");
        _battleBtn.clicked += () => SceneLoad("PVP");
        _gongBtn.clicked += () => SceneLoad("MakeRobot");
        _storeBtn.clicked += () => SceneLoad("Store");
        _garageBtn.clicked += () => SceneLoad("Garage");
    }

    void SceneLoad(string sceneString)
    {
        //로딩창 만드세요!
        SceneManager.LoadScene(sceneString);
    }

}
